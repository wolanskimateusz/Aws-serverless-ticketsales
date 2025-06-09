import { useAuth } from "../../Context/useAuth"
import SearchBar from "../SearchBar/SearchBar"
import './Header.css';  // Import pliku CSS

function Header() {

  const { isLoggedIn, logout } = useAuth()

  return (
    <header className="bg-dark py-3">
      <div className="container d-flex justify-content-between align-items-center">
        {/* Logo */}
        <div className="logo d-none d-lg-block"> 
          <a href="/" className="navbar-brand text-white me-2">
            <img src="/path/to/logo.png" alt="Logo" style={{ height: '50px' }} />
          </a>
        </div>

        {/* Przyciski menu i wyszukiwarka */}
        <nav className="navbar navbar-expand-lg w-100">
          {/* Przycisk hamburgera */}
          <button className="navbar-toggler custom-toggler ml-auto" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
          </button>

          {/* Rozwijane menu */}
          <div className="collapse navbar-collapse ms-2" id="navbarNav">
            <ul className="navbar-nav mx-auto">
              <li className="nav-item">
                <a href="/" className="nav-link text-white">Główna</a>
              </li>
              <li className="nav-item">
                <a href="/events" className="nav-link text-white">Wydarzenia</a>
              </li>
              <li className="nav-item">
                <a href="/artists" className="nav-link text-white">Artyści</a>
              </li>
              <li className="nav-item">
                <a href="/genres" className="nav-link text-white">Gatunki</a>
              </li>
            </ul>

            {/* Wyszukiwarka w rozwijanym menu */}
            <div className="d-lg-none mb-3">
              <SearchBar />
            </div>
            
            {/* Wyszukiwarka na dużych ekranach */}
            <div className="d-none d-lg-block me-3">
              <SearchBar />
            </div>

            {/* Login / Logout */}
            <div className="d-flex align-items-center">
              {isLoggedIn() ? (
                <div className="d-flex">
                  <a href="/profile" className="btn btn-outline-light mr-2">Profil</a>
                  <button type="button" onClick={logout} className="btn btn-danger">Wyloguj się</button>
                </div>
              ) : (
                <div className="d-flex">
                  <a href="/login" className="btn btn-outline-light mr-2">Logowanie</a>
                  <a href="/register" className="btn btn-danger">Rejestracja</a>
                </div>
              )}
            </div>
          </div>
        </nav>

      </div>
    </header>
  )
}

export default Header;
