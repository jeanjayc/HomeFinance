import { type Transaction } from "../../types/Transaction";
import TransactionForm from "../../components/TransactionForm/TransactionForm";

type NewTransactionProps = {
    onAddTransaction: (transaction: Transaction) => void;
};

function NewTransaction({ onAddTransaction }: NewTransactionProps){
    return(
        <div className="page page--new-transaction">
            <h1 className="page__title">Novo lançamento</h1>
            <TransactionForm onAddTransaction={onAddTransaction} />
        </div>
    );
}

export default NewTransaction;