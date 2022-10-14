# Casos de Uso

- **Cadastro de Atividade Econômica Principal (Main Activity)**
  - Código (único)
  - Descrição (único)
  
- **Cadastro de Qualificação de Sócios (Company Partner Qualifications)**
  - Código (único)
  - Descrição (único)
  - Exemplos: Titular, Presidente, Diretor, Administrador
  
- **Cadastro de Cargos de Funcionários (Company Employee Positions)**
  - Código (único)
  - Descrição (único)
  - Exemplos: Gerente, Desenvolvedor, Vendedor, Etc
  
- **Cadastro de Empresa (Companies)**
  - CNPJ (único)
  - Nome Empresarial (único)
  - Porte (ME, Eireli, LTDA, etc.)
  - Atividade Econômica Principal (1-N)
  - Endereço
  - Sócios (N-N)
    - Id do Usuário
    - Id da Qualificação de Sócios
    - Data de Entrada
  - Funcionários (1-N)
    - Id do Usuário
    - Id do Cargo do Funcionário
    - Data de Entrada
  - Data de Criação
  - Data de Atualização
  
- **Cadastro de Usuários (Users)**
  - Nome (único)
  - E-mail (único)

## Regras dos Casos de Uso

- Um funcionário não pode estar em mais de uma empresa
- Não pode atribuir o funcionário mais de uma vez na mesma empresa
- Não pode atribuir o sócio mais de uma vez na mesma empresa
