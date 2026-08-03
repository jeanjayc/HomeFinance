import { Link } from "react-router-dom";

import { useTheme } from "../../context/ThemeContext";

import "./Navbar.css";

function Navbar() {
  const { theme, toggleTheme } = useTheme();

  return (
    <nav className="navbar">
      <Link to="/" className="navbar__link">
        Dashboard
      </Link>
      <Link to="/transactions" className="navbar__link">
        Lançamentos
      </Link>
      <Link to="/new" className="navbar__link">
        Novo
      </Link>
      <span className="navbar__spacer" />
      <button
        type="button"
        className="navbar__theme-btn"
        onClick={toggleTheme}
        aria-label={theme === "light" ? "Ativar modo escuro" : "Ativar modo claro"}
      >
        {theme === "light" ? "Modo escuro" : "Modo claro"}
      </button>
    </nav>
  );
}

export default Navbar;
