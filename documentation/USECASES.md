# Casos de Uso

- **Cadastro de Atividade Econômica Principal (Main Activity)**
  - Código (único)
  - Descrição
- **Cadastro de Qualificação de Sócios (Company Partner Qualifications)**
  - Código (único)
  - Descrição
- **Cadastro de Cargos de Funcionários (Company Employee Positions)**
  - Id (único)
  - Descrição
- **Cadastro de Empresa (Companies)**
  - Id (único)
  - CNPJ (único)
  - Nome Empresarial (único)
  - Natureza Jurídica (ME, Eireli, LTDA, etc.)
  - Atividade Econômica Principal (1-N)
  - Endereço
  - Data de Criação
  - Data de Atualização
  - Funcionários (1-N)
  - Sócios (N-N)
- **Cadastro de Sócios da Empresa (Company Partners)**
  - Id do Usuário
  - Id da Qualificação de Sócios
  - Data de Entrada
- **Cadastro de Funcionários da Empresa (Company Employee)**
  - Funcionários (1-N)
  - Id do Usuário
  - Id do Cargo do Funcionário
  - Data de Entrada

- **Cadastro de Usuários (Users)**
  - Id (único)
  - Nome (único)

## Regras dos Casos de Uso

- A empresa precisa ser criada com pelo menos 1 sócio
- Um funcionário não pode estar em mais de uma empresa
- Não pode atribuir o funcionário mais de uma vez na mesma empresa
- Não pode atribuir o sócio mais de uma vez na mesma empresa
