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

## :bulb: Melhores práticas

### :arrow_forward: Nomeando os testes
**Por que?**
<p>Padrões de nomenclatura são importantes para deixar explícitos a sua intenção com determinado teste.</p>

- O nome de seu teste deverá conter estas 3 partes:
  - Nome do método a ser testado;
  - O cenário em que este está sendo testado;
  - O comportamento esperado quando o cenário é chamado.
  
> Vamos lembrar que testes não apenas testam seu código, eles servem como documentação também.

### Exemplo:
**Ruim**
```csharp
[Fact]
public void Test_Single()
{
    var stringCalculator = new StringCalculator();

    var actual = stringCalculator.Add("0");

    Assert.Equal(0, actual);
}
```
**Ideal**
```csharp
[Fact]
public void Add_SingleNumber_ReturnsSameNumber()
{
    var stringCalculator = new StringCalculator();

    var actual = stringCalculator.Add("0");

    Assert.Equal(0, actual);
}
```

### :arrow_forward: Arrumando (arranging) os testes - (Triple A)
Um padrão comun ao desenvolver teste unitário consiste nas seguintes ações:
- **Arrange:** Iniciamos e configuramos os objetos na medida que são necessários, como inicializando variáveis, criamos os *test doubles*;
- **Act**: Esta etapa é onde rodamos de fato o nosso teste. Chamamos alguma função ou método que queremos por a prova.
- **Assert**: É onde verificamos se a operação realizada na etapa anterior (*Act*) surtiu o resultado esperado. Assim sabemos se o teste passou ou falhou.

### :arrow_forward: Escreva testes de aprovação mínima
O parâmetro de entrada tem que ser o mais simples possível para que o comportamento seja o alvo no simples olhar de um outro desenvolvedor.
E testes que possuem muitas informações obrigatórias, tem maior chance de introduzir erros ao testar e não deixam as coisas mais claras possíveis.

### :arrow_forward: Evite strings mágicas
Devemos prevenir a necessidade do leitor do teste ir até o código de fato só para que ele entenda o que essa variável representa.
> Mostre o que você está tentando **provar** ao invés do que você está tentando **realizar**.

### Exemplo:
**Ruim**
```csharp
[Fact]
public void Add_BigNumber_ThrowsException()
{
    var stringCalculator = new StringCalculator();

    // O que é este "1001"???
    Action actual = () => stringCalculator.Add("1001");

    Assert.Throws<OverflowException>(actual);
}
```

**Ideal**
```csharp
[Fact]
void Add_MaximumSumResult_ThrowsOverflowException()
{
    var stringCalculator = new StringCalculator();
    const string MAXIMUM_RESULT = "1001";

    // Muito melhor, não?
    Action actual = () => stringCalculator.Add(MAXIMUM_RESULT);

    Assert.Throws<OverflowException>(actual);
}
```

### :arrow_forward: Evite lógica nos seus testes
<p>Ao escrever testes, evite concatenação de strings e operadores lógicos como, `if`, `while`, `for`, `switch` etc.</p>

**Por que?**
<p>Para não introduzirmos bugs dentro do testes e focar no resultado final ao invés da implementação.<br>
Se a lógica for inevitável, considere quebrar seus testes em mais testes diferentes.</p>

| Fonte | Link |
| ----- | ---- |
| Microsoft (Responsabilidade)  | [Link](https://docs.microsoft.com/en-us/dotnet/core/testing/?pivots=xunit) |
| Microsoft (Melhores Práticas) | [Link](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices) |
| Martin Fowler (Mocks aren't stubs) | [Link](https://martinfowler.com/articles/mocksArentStubs.html) |
| Medium - Training Center, por Lucas Santos | [Link](https://medium.com/trainingcenter/testes-unit%C3%A1rios-mocks-stubs-spies-e-todas-essas-palavras-dif%C3%ADceis-f2765ac87cc8)
| Medium - Ship it, por Pablo Rodrigo Darde | [Link](https://medium.com/rd-shipit/test-doubles-mocks-stubs-fakes-spies-e-dummies-a5cdafcd0daf) |
