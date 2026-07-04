import "./Summary.css";

type SummaryProps = {
  income: number;
  expense: number;
  balance: number;
};

function Summary({ income, expense, balance }: SummaryProps) {
  const balanceClass = balance < 0 ? "summary__value--balance negative" : "summary__value--balance";

  return (
    <div className="summary">
      <div className="summary__card">
        <h4 className="summary__label">Receitas</h4>
        <p className={`summary__value summary__value--income`}>
          R$ {income.toFixed(2)}
        </p>
      </div>
      <div className="summary__card">
        <h4 className="summary__label">Despesas pagas</h4>
        <p className="summary__value summary__value--expense">
          R$ {expense.toFixed(2)}
        </p>
      </div>
      <div className="summary__card">
        <h4 className="summary__label">Saldo</h4>
        <p className={`summary__value ${balanceClass}`}>
          R$ {balance.toFixed(2)}
        </p>
      </div>
    </div>
  );
}

export default Summary;
