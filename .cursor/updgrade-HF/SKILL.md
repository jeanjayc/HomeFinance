# Home Finance Project Skill

Você está trabalhando no projeto Home Finance.

## Objetivo

Integrar completamente o frontend já existente com o backend existente e deixar todo o ambiente pronto para execução através do Docker.

## Processo obrigatório

### 1. Analisar a estrutura do projeto

Antes de qualquer alteração:

- Mapear toda a estrutura de diretórios.
- Identificar:
  - Backend
  - Frontend
  - Arquivos de configuração
  - Infraestrutura AWS
  - Docker existente
  - CloudFormation
  - GitHub Actions

Gerar um resumo da arquitetura encontrada.

### 2. Entender o Frontend

Localizar o frontend já implementado.

Identificar:

- Framework utilizado
- Rotas
- Componentes
- Serviços HTTP
- Variáveis de ambiente
- Dependências

Verificar quais endpoints já estão sendo consumidos.

### 3. Entender o Backend

Identificar:

- Controllers
- Endpoints
- DTOs
- Casos de uso
- Serviços
- Banco de dados
- Configurações AWS

Gerar um mapeamento dos endpoints disponíveis.

### 4. Realizar integração Front x Back

Conectar todas as telas existentes aos endpoints corretos.

Garantir:

- Tratamento de erros
- Loading states
- Tipagem correta
- DTOs compatíveis
- URLs configuráveis por ambiente

Caso algum endpoint esteja faltando:

- Implementar no backend.
- Seguir o padrão arquitetural existente.

### 5. Dockerização

Criar:

- Dockerfile Backend
- Dockerfile Frontend
- docker-compose.yml

O ambiente deve subir com um único comando:

docker compose up

### 6. Banco de dados

Caso exista banco local:

Configurar container para:

- PostgreSQL


Garantir persistência através de volumes.

### 7. Variáveis de ambiente

Criar:

.env.example

Separando:

Backend:
- Database
- JWT
- URLs

Frontend:
- API_URL

### 8. Qualidade

Executar e corrigir:

- Build Backend
- Build Frontend
- Erros de compilação
- Imports quebrados
- Dependências faltantes

Nenhum warning crítico deve permanecer.

### 9. Documentação

Atualizar README.md contendo:

## Como executar

docker compose up --build

## URLs

Frontend:
http://localhost:3000

Backend:
http://localhost:5000

## Estrutura

Descrição resumida dos módulos.

## Restrições

- Não remover funcionalidades existentes.
- Não alterar regras de negócio sem necessidade.
- Não criar código duplicado.
- Reutilizar componentes existentes.
- Seguir princípios SOLID.
- Seguir Clean Architecture quando aplicável.
- Preferir soluções simples.

## Resultado esperado

Ao final:

- Frontend integrado ao backend.
- Projeto compilando.
- Containers funcionando.
- README atualizado.
- Nenhum erro de build.