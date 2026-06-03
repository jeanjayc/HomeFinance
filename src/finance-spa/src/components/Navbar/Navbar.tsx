import { Link } from "react-router-dom";

function Navbar() {
    return (
        <nav
          style={{
            background: "#1e293b",
            padding: "16px",
            display: "flex",
            gap: "16px",
          }}
        >
        <Link to="/" style={{ color: "white", textDecoration: "none" }}>Dashboard</Link>
        <Link to="/transactions" style={{ color: "white", textDecoration: "none" }}>
        Lançamentos
      </Link>

      <Link to="/new" style={{ color: "white", textDecoration: "none" }}>
        Novo
      </Link>
    </nav>
    );
}

export default Navbar;