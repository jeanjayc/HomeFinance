import { type Transaction } from "../../types/Transaction";
import TransactionList from "../../components/TransactionList/TransactionList";

type TransactionProps = {
    transactions: Transaction[];
    onTogglePaid: (id: string) => void;
};

function Transactions({ transactions, onTogglePaid }: TransactionProps) {
    return (
        <div className="page page--transactions">
            <h1 className="page__title">Lançamentos</h1>
            <TransactionList transactions={transactions} onTogglePaid={onTogglePaid} />
        </div>
    );
}

export default Transactions;
