type PageStatusProps = {
  loading?: boolean;
  error?: string | null;
  onRetry?: () => void;
};

function PageStatus({ loading, error, onRetry }: PageStatusProps) {
  if (loading) {
    return <p className="page-status">Carregando...</p>;
  }

  if (error) {
    return (
      <div className="page-status page-status--error">
        <p>{error}</p>
        {onRetry && (
          <button type="button" onClick={onRetry}>
            Tentar novamente
          </button>
        )}
      </div>
    );
  }

  return null;
}

export default PageStatus;
