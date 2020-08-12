# Testes unitário

### :arrow_forward: Responsabilidade
São responsáveis por testar os componentes e métodos individuais de um software.
Devem testar apenas o código que o desenvolvedor tem controle, ou seja, não deverá testar a parte de infraestrutura (banco de dados, sistema de arquivos e recursos de rede).

Pode nos ajudar mostrando que o nosso código está muito acoplado (muito ruim).

### :arrow_forward: Características de um bom teste unitário
- **Rápido (Fast):** Deverá tomar pouco tempo ao rodar, milisegundos sendo o orientado pela doc da Microsoft;
- **Isolado (Isolated):** Tem que ser autônomo, não pode possuir dependências como banco de dados, sistema de arquivos;
- **Repetível (Repeatable):** Deve ser consistente nos resutlados, ou seja, cada vez que for executado, deverá retornar sempre o mesmo resultado;
- **Auto verificação (Self-checking):** Deve ser capaz de detectar automaticamente se executou com êxito ou obteve falha, sem que haja interação humana;
- **Feito a tempo (Timely):** Um teste não deverá levar mais tempo de desenvolvimento do que o código a ser testado, se isso vir a ocorrer, devemos considerar melhorar o código.

### :arrow_forward: Cobertura de Código
Não quer dizer que se você possuir uma grande cobertura de testes que seu código está impecável ou que seja um indicador de sucesso, apenas representa a quantidade de código que está sendo coberta pelos testes.

### :arrow_forward: Diferenciando *Fakes*, *Dummies*, *Stubs*, *Spies* e *Mocks*
- ***Fakes:*** Objetos que possuem implementação, porém com o objetivo de diminuir a complexidade e/ou tempo de execução de alguns processos para acelerar os testes, porém nunca usado em produção. 
  * **Exemplos:** banco de dados em memória, web service, camada de serviços

- ***Dummies:*** Objetos que são passados mas nunca realmente são usados. Geralmente são utilizados para preencher lista de parâmetros.

- ***Stubs:*** Provê respostas prontas para chamadas feitas durante o teste, ou seja, um objeto que você criará para testar seu código sem lidar com a dependência direta.
  * **Obs.:** Geralmente não são utilizados na hora do *assert*.</p>

- ***Spies:*** São *stubs* que também guardam informações das interações com outros métodos.

- ***Mocks:*** Objetos pré-programados com informações que formam uma especificação das chamadas que esperam receber.

### :arrow_forward: Nomeando os testes
- todo...

### :arrow_forward: Arrumando (arranging) os testes
- todo...

### :arrow_forward: Write minimally passing tests
- todo...

### :arrow_forward: Avoid magic strings
- todo...

### :arrow_forward: Avoid logic in tests
- todo...

### :arrow_forward: Prefer helper methods to setup and teardown
- todo...

### :arrow_forward: Avoid multiple asserts
- todo...

### :arrow_forward: Validate private methods by unit testing public methods
- todo...

### :arrow_forward: Stub static references
- todo...

| Fonte | Link |
| ----- | ---- |
| Microsoft (Responsabilidade)  | [Link](https://docs.microsoft.com/en-us/dotnet/core/testing/?pivots=xunit) |
| Microsoft (Melhores Práticas) | [Link](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices) |
| Martin Fowler (Mocks aren't stubs) | [Link](https://martinfowler.com/articles/mocksArentStubs.html) |
| Medium - Training Center, escrito por Lucas Santos | [Link](https://medium.com/trainingcenter/testes-unit%C3%A1rios-mocks-stubs-spies-e-todas-essas-palavras-dif%C3%ADceis-f2765ac87cc8)
