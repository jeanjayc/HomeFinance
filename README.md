# Home Finance

Aplicação de finanças pessoais com backend .NET 8, SPA React e PostgreSQL.

## Como executar

Com [Docker](https://www.docker.com/) instalado, na raiz do repositório:

```bash
cp .env.example .env
docker compose up --build
```

Na primeira subida, a API aplica as migrations do banco automaticamente.

Os dados do PostgreSQL persistem no volume Docker `hf_pg_data`. Para resetar o banco (por exemplo, após mudança de schema em desenvolvimento):

```bash
docker compose down -v
docker compose up --build
```

## URLs

| Serviço    | URL                      |
|-----------|--------------------------|
| Frontend  | http://localhost:3000    |
| Backend   | http://localhost:5000    |
| Swagger   | http://localhost:5000/swagger (Development) |
| PostgreSQL| localhost:5432           |

## Desenvolvimento local (sem Docker)

### Backend

```bash
# Subir PostgreSQL (ou usar docker compose up postgres -d)
export ConnectionStrings__Connection="Host=localhost;Port=5432;Database=HomeFinance;Username=postgres;Password=postgres"

cd src/HomeFinance.Api
dotnet run
```

API em http://localhost:5000 (configure `launchSettings.json` ou `ASPNETCORE_URLS` se necessário).

### Frontend

```bash
cd src/finance-spa
cp .env.example .env
npm install
npm run dev
```

SPA em http://localhost:3000 com proxy `/api` para o backend.

## Estrutura

| Pasta / projeto | Descrição |
|-----------------|-----------|
| `src/HomeFinance.Models` | Entidades de domínio (`Finances`) |
| `src/HomeFinance.Application` | Serviços de aplicação (`FinancesService`) |
| `src/HomeFinance.Infra` | EF Core, Dapper, DTOs, repositórios, migrations |
| `src/HomeFinance.Api` | API REST para o SPA |
| `src/HomeFinance.MVC` | Interface web legada (Razor) |
| `src/finance-spa` | Frontend React + Vite |
| `src/HomeFinanceTests` | Testes unitários |

## Variáveis de ambiente

Veja [.env.example](.env.example) para PostgreSQL, connection string, CORS e `VITE_API_URL`.

## API (finanças)

Rotas em `api/Financas/[action]`:

- `GET BuscarTodasFinancas`
- `GET BuscarFinancaPorId?id={guid}`
- `POST CriarFinanca`
- `PUT AtualizarFinanca`
- `DELETE DeletarFinanca?id={guid}`
- `POST AlternarPago?id={guid}`

O campo `MesReferencia` é o **mês calendário de 1 a 12** (contrato único entre API, banco e SPA).

### SPA

| Rota | Descrição |
|------|-----------|
| `/` | Dashboard (resumo do mês atual) |
| `/transactions` | Lista com editar, excluir e marcar pago |
| `/transactions/:id/edit` | Edição de lançamento |
| `/new` | Novo lançamento |
