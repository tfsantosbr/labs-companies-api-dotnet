# Documentação da API

API de cadastro de **Empresas**, com seus respectivos **Sócios**

## Regras de Negócio

- A empresa precisa ser criada com pelo menos 1 sócio
- O CNPJ poderá ser informado SOMENTE no cadastro da empresa
- Não pode atribuir o sócio mais de uma vez na mesma empresa
- Disponibilizar endpoints para listar as atividades econômicas
- Disponibilizar endpoints para listar as qualificações dos sócios
- Disponibilizar endpoints para gerenciamento dos sócios (Create, Read, Update, Delete)
- Disponibilizar endpoints para o gerenciamento da empresa (Create, Read, Update, Delete)
- Disponibilizar endpoints para o gerencimaneto dos sócios da empresa (Create, Read, Delete)
  
## Campos e Relacionamentos

```bash
# Cadastro de Atividade Econômica Principal (Main Activity)
- Código (único)
- Descrição

# Cadastro de Qualificação de Sócios (Company Partner Qualifications)
- Código (único)
- Descrição

# Cadastro de Sócios (Partners)
- Id (único)
- Nome (único)

# Cadastro de Empresa (Companies)
- Id (único)
- CNPJ (único)
- Nome Empresarial (único)
- Natureza Jurídica (ME, Eireli, LTDA, etc.)
- Atividade Econômica Principal (1-N)
- Endereço
- Data de Criação
- Data de Atualização
- Telefones (1-N)
- Sócios (N-N)

# Cadastro de Sócios da Empresa (Company Partners)
- Id do Usuário
- Id da Qualificação de Sócios
- Data de Entrada
```
