# Home Finance - Frontend UX Improvement Skill

## Contexto

A aplicação Home Finance está funcional, porém existe um problema de usabilidade na tela de listagem de lançamentos financeiros.

Atualmente, quando uma finança é criada utilizando o tipo `Installment` (parcelada), cada parcela é exibida individualmente na listagem principal.

Isso gera alguns problemas:

* Poluição visual.
* Dificuldade para localizar lançamentos.
* Sensação de duplicidade de informações.
* Experiência ruim quando existem muitas parcelas.
* O usuário perde a visão de que todas as parcelas pertencem à mesma compra ou compromisso financeiro.

O objetivo desta melhoria é tornar a experiência mais intuitiva através de agrupamento visual.

---

## Objetivo

Implementar agrupamento visual para lançamentos do tipo `Installment`.

Todas as parcelas pertencentes ao mesmo parcelamento devem ser exibidas como um único grupo expansível.

---

## Regras de Negócio

### Agrupamento

Quando uma finança possuir o tipo:

```text
Installment
```

As parcelas devem ser agrupadas utilizando o identificador comum do parcelamento.

O agrupamento deve gerar apenas um item visível inicialmente.

Exemplo:

Parcelas Apartamento ▼

ou

Notebook Gamer ▼

ou

Cartão Nubank ▼

---

### Expansão

Ao clicar no grupo:

* Expandir as parcelas.
* Exibir todas as parcelas pertencentes ao parcelamento.
* Permitir recolher novamente.

Exemplo expandido:

Parcelas Apartamento ▼

☐ Maio/2026
☐ Junho/2026
☐ Julho/2026

---

### Nome das Parcelas

As parcelas devem exibir:

* Mês
* Ano

Obtidos a partir da data de vencimento.

Exemplo:

Maio/2026
Junho/2026
Julho/2026

Evitar exibir:

Parcela 1
Parcela 2
Parcela 3

Pois o usuário normalmente se orienta pelo mês de vencimento.

---

### Status de Pagamento

Cada parcela deve continuar exibindo seu status individual.

Exemplo:

☑ Maio/2026
☐ Junho/2026
☐ Julho/2026

Onde:

* Marcado = pago
* Desmarcado = pendente

---

### Resumo do Grupo

O item agrupado deve apresentar informações resumidas relevantes.

Exemplo:

Parcelas Apartamento ▼

3 parcelas
1 paga
2 pendentes

ou

Parcelas Apartamento ▼

R$ 900,00 pagos
R$ 1.800,00 pendentes

O Cursor deve reutilizar os dados já existentes na aplicação sempre que possível.

---

## UX

### Estado recolhido

Exibir apenas:

* Nome do parcelamento
* Quantidade de parcelas
* Indicador visual de expansão

Exemplo:

📁 Parcelas Apartamento (3)

---

### Estado expandido

Exibir:

* Parcelas individuais
* Mês/Ano
* Status
* Valor

Exemplo:

📁 Parcelas Apartamento (3)

☑ Maio/2026 - R$ 900,00
☐ Junho/2026 - R$ 900,00
☐ Julho/2026 - R$ 900,00

---

## Requisitos Técnicos

Antes de implementar:

1. Analisar a estrutura atual do frontend.
2. Identificar o componente responsável pela listagem.
3. Identificar como o backend diferencia uma finança Installment.
4. Verificar se já existe algum identificador comum entre parcelas.

Exemplos:

* InstallmentId
* ParentId
* GroupId
* TransactionGroupId

Caso não exista agrupamento possível:

* Propor a melhor estratégia.
* Implementar a solução mínima necessária.
* Evitar alterar regras de negócio existentes.

---

## Performance

Evitar:

* Loops desnecessários.
* Reprocessamento da lista a cada render.
* Requisições adicionais ao backend.

Preferir:

* Agrupamento em memória.
* useMemo quando aplicável.
* Componentes reutilizáveis.

---

## Resultado Esperado

Ao final da implementação:

* Parcelamentos aparecem agrupados.
* O usuário consegue expandir e recolher parcelas.
* Cada parcela exibe mês/ano corretamente.
* O status individual continua visível.
* Não há alteração no comportamento de lançamentos que não são Installment.
* O código segue os padrões existentes do projeto.
* Não existem erros de build ou warnings críticos.
