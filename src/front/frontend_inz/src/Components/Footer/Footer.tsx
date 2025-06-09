
interface Props  {}

function Footer()
{
    return <>
     <footer className="bg-dark text-light py-4">
      <div className="container">
        <div className="row">
          {/* Logo i opis */}
          <div className="col-md-4 mb-3">
            <h5 className="text-uppercase">EventHub</h5>
            <p>
              Twoje miejsce do znalezienia biletów na najlepsze wydarzenia!
            </p>
          </div>


          {/* Media społecznościowe */}
          <div className="col-md-4 mb-3">
            <h5 className="text-uppercase">Bądz z nami na bieżąco</h5>
            <div>
              <a href="https://facebook.com" className="text-light me-3"><i className="bi bi-facebook"></i> Facebook</a>
              <a href="https://twitter.com" className="text-light me-3"><i className="bi bi-twitter"></i> Twitter</a>
              <a href="https://instagram.com" className="text-light"><i className="bi bi-instagram"></i> Instagram</a>
            </div>
          </div>
        </div>

        <hr className="bg-light" />

        {/* Prawa autorskie */}
        <div className="text-center">
          <p className="mb-0">© 2024 EventHub. All Rights Reserved.</p>
        </div>
      </div>
    </footer>
    </>
}

export default Footer