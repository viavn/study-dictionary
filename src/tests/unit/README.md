# Testes unitário

### Responsabilidade
São responsáveis por testar os componentes e métodos individuais de um software.
Devem testar apenas o código que o desenvolvedor tem controle, ou seja, não deverá testar a parte de infraestrutura (banco de dados, sistema de arquivos e recursos de rede).

Pode nos ajudar mostrando que o nosso código está muito acoplado (muito ruim).

### Características de um bom teste unitário
- **Rápido (Fast):** Deverá tomar pouco tempo ao rodar, milisegundos sendo o orientado pela doc da Microsoft;
- **Isolado (Isolated):** Tem que ser autônomo, não pode possuir dependências como banco de dados, sistema de arquivos;
- **Repetível (Repeatable):** Deve ser consistente nos resutlados, ou seja, cada vez que for executado, deverá retornar sempre o mesmo resultado;
- **Auto verificação (Self-checking):** Deve ser capaz de detectar automaticamente se executou com êxito ou obteve falha, sem que haja interação humana;
- **Feito a tempo (Timely):** Um teste não deverá levar mais tempo de desenvolvimento do que o código a ser testado, se isso vir a ocorrer, devemos considerar melhorar o código.

### Cobertura de Código
Não quer dizer que se você possuir uma grande cobertura de testes que seu código está impecável ou que seja um indicador de sucesso, apenas representa a quantidade de código que está sendo coberta pelos testes.

### Diferenciando *Fake*, *Mock* e *Stub*
- ***Fake:*** Objetos que possuem implementação, porém com o objetivo de diminuir a complexidade e/ou tempo de execução de alguns processos para acelerar os testes, porém nunca usado em produção. 
  * **Exemplos:** banco de dados em memória, web service, camada de serviços

- ***Stubs:*** provide canned answers to calls made during the test, usually not responding at all to anything outside what's programmed in for the test.

- ***Mocks:*** objects pre-programmed with expectations which form a specification of the calls they are expected to receive.

### Nomeando os testes
- todo...

### Arrumando (arranging) os testes
- todo...

### Write minimally passing tests
- todo...

### Avoid magic strings
- todo...

### Avoid logic in tests
- todo...

### Prefer helper methods to setup and teardown
- todo...

### Avoid multiple asserts
- todo...

### Validate private methods by unit testing public methods
- todo...

### Stub static references
- todo...

| Fonte | Link |
| ----- | ---- |
| Microsoft (Responsabilidade)  | [Link](https://docs.microsoft.com/en-us/dotnet/core/testing/?pivots=xunit) |
| Microsoft (Melhores Práticas) | [Link](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices) |
| Martin Fowler (Mocks aren't stubs) | [Link](https://martinfowler.com/articles/mocksArentStubs.html) |
