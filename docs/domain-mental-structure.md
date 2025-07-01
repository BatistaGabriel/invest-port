# Estrutura Mental da Domain

## Entities/
- Portfolio.cs (Aggregate Root)
- Investment.cs  
- User.cs

## ValueObjects/
- Money.cs
- AssetCode.cs
- Quantity.cs

## Interfaces/
- IPortfolioRepository.cs
- IPriceService.cs

## Services/
- PriceCalculationService.cs
- RiskAssessmentService.cs

## Events/ (para Event Sourcing)
- InvestmentCreatedEvent.cs
- InvestmentSoldEvent.cs
