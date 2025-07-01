# Entidades

## Entidades:

- Portfolio
- Investment
- User

## Value Objects (sem identidade, imutáveis):

- Money (valor + moeda)
- AssetCode (PETR4, VALE3, etc)
- Quantity (quantidade de ações)
- AssetType (STOCK, CRYPTO, BOND)

## Aggregates (Agregados)
Portfolio (entidade) é o aggregate root porque:

- Controla todas as operações de investimento
- Garante consistência das regras de negócio
- É o "portão de entrada" para mudanças

## Domain Services
Operações que não pertencem a uma entidade específica:

- PriceCalculationService (calcular valor atual do portfolio)
- RiskAssessmentService (avaliar risco dos investimentos)