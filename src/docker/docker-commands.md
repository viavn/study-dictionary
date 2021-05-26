# Containers

## Apagar container que não estão sendo utilizados
1. Executar:
    * `docker container prune`

<br/>
<br/>

# Volumes

## Volume do tipo *bind*
1. Criar pasta: /opt/my-folder-to-bind
2. Executar:
    * `docker run -ti --mount type=bind,src=/opt/my-folder-to-bind,dst=/destination-folder debian`
        * `type=bind` - já tenho um diretório e quero que este seja montado dentro do container

    2.1. Para fazer um bind e deixar a pasta como read-only, executar comando abaixo:
    * `docker run -ti --mount type=bind,src=/mnt/c/Users/osv/docker-study/volumes/volume-bind,dst=/destination-folder,ro debian`
        * `ro` = Read Only
            * Ao tentar apagar arquivo que está dentro de `destination-folder`, um erro deverá aparecer:
                * `rm: cannot remove 'teste2': Read-only file system`

3. Dentro do container execute o seguinte comando para saber os mounts:
    * `df -h`

## Volume do tipo *volume*
1. Criar volume:
    * `docker volume create {VOLUME_NAME}`
    * `docker volume ls`

    1.1. Caso esteja utilizando WSL 2, seus volumes serão montados em:
    * `\\wsl$\docker-desktop-data\version-pack-data\community\docker\volumes`

2. Executar:
    * `docker run -ti --mount type=volume,src={VOLUME_NAME},dst=/my-folder-inside-container debian`

3. Para remover o volume, primeiro devemos remover o container completamente:
    * `docker stop {ID_CONTAINER}` ou `docker rm -f {ID_CONTAINER}`
    
    3.1 Depois basta remover o volume:
    * `docker volume rm {VOLUME_NAME}`

## Apagar volumes que não estão sendo utilizados
1. Executar:
    * `docker volume prune`

* **MUITO CUIDADO** pois vamos perder todos os dados.

<br/>
<br/>

## Alternativa para Volumes
Antes da feature de volumes ser criada, antigamente o pessoal criava um container que conteria os dados dos outros containers. Por exemplo:

1. Crie um container, porém não iremos executá-lo:
    * `docker container create -v /data --name dbdados centos`

2. Criar e executar 2 containers do POSTGRESQL:
    * `docker container run -d -p 5432:5432 --name pgsql1 --volumes-from dbdados -e POSTGRESQL_USER=docker -e POSTGRESQL_PASS=docker -e POSTGRESQL_DB=docker kamui/postgresql`
    * `docker container run -d -p 5433:5432 --name pgsql2 --volumes-from dbdados -e POSTGRESQL_USER=docker -e POSTGRESQL_PASS=docker -e POSTGRESQL_DB=docker kamui/postgresql`

### A versão mais atual deste passo, seria:
1. Criando um volume:
    * `docker volume create dbdados`

2. Criando e executando os containers POSTGRESQL:
    
    2.1. `docker run -d -p 5432:5432 --name pgsql1 --mount type=volume,src=postgres-data,dst=/data -e POSTGRESQL_USER=docker -e POSTGRESQL_PASS=docker -e POSTGRESQL_DB=docker kamui/postgresql`
    
    2.2. `docker run -d -p 5433:5432 --name pgsql2 --mount type=volume,src=postgres-data,dst=/data -e POSTGRESQL_USER=docker -e POSTGRESQL_PASS=docker -e POSTGRESQL_DB=docker kamui/postgresql`

<br/>
<br/>

## Fazendo backup de dados do volume
Criando um container apenas para empacotar os dados do volume e depois jogamos para uma pasta já criada no HOST.

### Resumo do comando a ser executado
Para isso, criamos um container com um mount do tipo volume, onde ele vai olhar para o volume compartilhado pelos containers e apontamos para uma pasta qualquer como destino; depois vamos passar um outro mount do tipo bind, onde apontaremos uma pasta já criada no HOST, que é a pasta source que armazenará o backup .tar gerado e passamos uma pasta a ser criada como destino; por fim, passamos um comando para empacotar os arquivos que estão localizados no diretório destino passados no primeiro comando mount do tipo volume.

1. Criar um diretório de backup
    * `mkdir /opt/backup`

2. Criando um container "auxiliar"
    
    2.1. Forma antiga:
    * `docker run -ti --volumes-from dbdados -v /opt/backup:/backup debian tar -cvf /backup/backup.tar /data`

    2.2. Forma nova
    * `docker run -ti --mount type=volume,src=dbdados,dst=/data --mount type=bind,src=/opt/backup,dst=/backup debian tar cvf /backup/bkp-banco.tar /data`

<br/>
<br/>

## Criando uma imagem a partir de uma container em execução

1. Execute um container:
    * `docker run -ti debian`

    1.1. Instale algum pacote que não venha na imagem default:
    * `apt-get update && apt-get install curl`

    1.2. Saia do container sem que ele pare sua execução:
    * `CTRL+P+Q`

2. Execute um commit:
    * `docker commit -m "debian com curl" {ID_CONTAINER}`

    2.1. Porém ao executar `docker images`, você irá notar que a imagem foi criada sem uma tag. Para adicionar uma tag basta:
    * `docker image tag {ID_IMAGE} {SUA_TAG}`

## Listando consumo de memória de um container
1. Execute:
    *   `docker container stats`

<br/>
<br/>

# Dockerfile

## Comandos

* `ADD` => Copia novos arquivos, diretórios, arquivos TAR ou arquivos remotos e os adicionam ao filesystem do container;

* `CMD` => Executa um comando, diferente do RUN que executa o comando no momento em que está "buildando" a imagem, o CMD executa no início da execução do container;

* `LABEL` => Adiciona metadados a imagem como versão, descrição e fabricante;

* `COPY` => Copia novos arquivos e diretórios e os adicionam ao filesystem do container;

* `ENTRYPOINT` => Permite você configurar um container para rodar um executável, e quando esse executável for finalizado, o container também será;

* `ENV` => Informa variáveis de ambiente ao container;

* `EXPOSE` => Informa qual porta o container estará ouvindo;

* `FROM` => Indica qual imagem será utilizada como base, ela precisa ser a primeira linha do Dockerfile;

* `MAINTAINER` => Autor da imagem; 

* `RUN` => Executa qualquer comando em uma nova camada no topo da imagem e "commita" as alterações. Essas alterações você poderá utilizar nas próximas instruções de seu Dockerfile;

* `USER` => Determina qual o usuário será utilizado na imagem. Por default é o root;

* `VOLUME` => Permite a criação de um ponto de montagem no container; indica que determinado diretório no container será um volume

* `WORKDIR` => Responsável por mudar do diretório / (raiz) para o especificado nele;

<br/>
<br/>

# Docker Machine

## Instalação
* `https://docs.docker.com/machine/install-machine/`

## Executando via AWS EC2
1. Documentação:
    * `https://docs.docker.com/machine/examples/aws/`

2. Seguir passo a passo do link:
    * `https://medium.com/@fidelissauro/docker-swarm-02-quickstart-do-seu-cluster-de-ec2-na-amazon-aws-com-docker-1394d365cb04`

3. Executar o seguinte comando:
    * `docker-machine create --driver amazonec2 --amazonec2-vpc-id vpc-04b4ea4c98a5d3b06 --amazonec2-subnet-id subnet-0d97c2dc036fe869d --amazonec2-region us-east-1 --amazonec2-zone c --amazonec2-instance-type "t2.micro" --amazonec2-security-group swarm-cluster aws01`

<br/>
<br/>

## Docker Swarm
Crie 3 máquinas pelo `docker-machine` utilizando seu driver favorito. Em meus exemplos, eu escolhi criar utilizando o driver `amazonec2`.

1. Conecte-se em cada máquina através de um destes comandos:
    
    1.1. Via `ssh`:
    *   `docker-machine ssh {NOME-MAQUINA}`

    1.2. Ou executando este comando que exportará as variáveis para sua máquina:
    *  `eval $(docker-machine env {NOME-MAQUINA})`

2. Na primeira máquina, vamos iniciá-la como **manager**:
    *   `docker swarm init`

    2.1. Copie a linha que foi gerada. Esta linha possui o token para acessar como um worker dentro do cluster. Vá em outro terminal em que cole:
    *   `docker swarm join --token SWMTKN-1-1o2q2q19usz6op7371l6jaehdzn5eksvh4nc7pr5s160d18wym-4urdrz90hck8j0okm082hlxat 172.31.0.180:2377`

3. Execute o comando abaixo no bash em que foi criado o cluster, ou seja, o nó que é o manager, para visualizar os nós e seus status.
    *   `docker node ls`

4. Para promover um worker para manager, execute a seguinte linha de comando no bash do manager:
    *   `docker node promote {NOME-MAQUINA}`

    4.1. Para voltar o node a ser um worker, execute:
    *  `docker node demote {NOME-MAQUINA}`

    4.1.1. **Obs.:** Lembre-se, para remover um node, este deve ser um worker senão um erro irá aparecer na tela.
    *   Para remover execute: `docker node rm -f {NOME-MAQUINA}`
        *  `-f`: para forçar caso o node não tenha saído do cluster swarm.
    * Para sair do cluster, execute o comando abaixo no bash de sua segunda máquina:
        *   `docker swarm leave`
        * E depois no bash do manager, você poderá remover sem a flag **-f**:
            * `docker node rm {NOME-MAQUINA}`
    
5. Para recuperar o comando para se conectar ao cluster
    
    5.1. Como um **worker**:
    *   `docker swarm join-token worker`
    
    5.2. Como um **manager**:
    *   `docker swarm join-token manager`

6. Para atualizar tokens e tornar o antigo inválido, execute:
    
    6.1. Para um token de **worker**:
    *   `docker swarm join-token --rotate worker`
    
    6.2. Para um token de **manager**:
    *   `docker swarm join-token --rotate manager`

## Docker Swarm - Node
1. Crie um serviço
    *   `docker service create --name webserver replicas 3 -p 8080:80 nginx`

    1.1. Listar os containers de cada nó:
    *   `docker service ps webserver`

2. Pausar a criação de novas instâncias:
    2.1 Execute:
    *   `docker node update --availability pause {NOME-MAQUINA}`

    2.2. Escale mais 10 instâncias deste serviço:
    *   `docker service scale webserver=10`

    2.3. Agora perceba que nenhum container foi criado para a máquina que você pausou:
    *   `docker service ps webserver`

    2.4. Desligando as instâncias de um determinado nó:
    *   `docker node update --availability drain {NOME-MAQUINA}`

    2.5: Voltando ao status ativo:
    *   `docker node update --availability active {NOME-MAQUINA}`

3. Removendo o serviço:
    *   `docker service rm webserver`

## Docker Swarm - Services
1. Listando serviços criados
    * `docker service ls`

2. Inspecionar um serviço
    * `docker service inspect {NOME-SERVIÇO} --pretty`
        * ***--pretty***: Formatando saída

## Docker Swarm - Network
Vamos criar uma rede para que o nosso cluster se comunique. Para isso, devemos criar uma rede do tipo ***overlay***:

* Obs.: Lembre de já ter executado `docker swarm init`

1. Execute:
    * `docker network create -d overlay {NOME-NETWORK}`

2. Execute:
    * `docker service create --name nginx1 -p 8080:80 --network {NOME-NETWORK} nginx`
    * `docker service create --name nginx2 -p 8088:80 --network {NOME-NETWORK} nginx`

3. Entrar em algum container e executar um curl apontando para o nome do node, como:
    
    3.1. Imagine que entramos dentro do container que está executando o ***nginx1***
    * `curl nginx2`

## Docker Swarm - Secret
Secrets têm a responsabilidade de armazenar dentro de containers informações sensíveis como passwords

1. Criando secrets:
    
    1.1. Secret a partir de um texto:
    * `echo -n "GIROPOPS STRIGUS GIRUS" | docker secret create {NOME-SECRET} -`
        * `-n`: Evitar quebra de linha
        * `| (pipe)`: pega a saída do comando da direita e manda para o comando após o ***pipe***;

    1.2. Secret a partir de um arquivo:
    * `vim {NOME-ARQUIVO.EXTENSÃO}` - Coloque algum texto
    * `docker secret create {NOME-SECRET} {NOME-ARQUIVO.EXTENSÃO}`

2. Criando um service com um secret
    * `docker service create --name nginx -p 8080:80 --secret {NOME-SECRET} nginx`

    2.1. Entre dentro do container e veja que o secret foi criado na seguinte pasta:
    * `/run/secrets`

    2.2. Adicionando um novo secret de um servico em execução:
    * `docker service update --secret-add secret-text {NOME-SERVIÇO}`
    
    2.3. Removendo um secret de um servico em execução:
    * `docker service update --secret-rm secret-text {NOME-SERVIÇO}`

3. Criando um service com secret com nome diferente e com permissões:
    *  `docker service create --name nginx1 -p 8080:80 --secret src={NOME-SECRET},target={NOVO-NOME},uid=200,gid=200,mode=0400 nginx`
        * `uid=200`: id de usuário específico do container
        * `gid=200`: id de grupo específico do container
        * `mode=0400`: apenas leitura do administrador

# Docker-compose && Stack
1. Criando uma stack a partir de um docker-compose file:
    * Crie seu docker-compose.yml e execute:
        * `docker stack deploy -c docker-compose.yml {NOME-STACK}`