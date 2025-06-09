import axios from "axios";
import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";


interface eventData{
 name: string
 date: string
 localization: string
 normalPrice: number
 vipPrice: number
}

  
function BuyTicket()
{
  const [eventData, setEventData] = useState<eventData>()
  const {id} = useParams<{ id: string }>();
  const [quantities, setQuantities] = useState<{ normalny: number; vip: number }>({
    normalny: 0,
    vip: 0,
  });
  const ownerData = localStorage.getItem("user");
  const owner = ownerData ? JSON.parse(ownerData).userName : ""
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<boolean>(false);
  const navigate = useNavigate()

  const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"

  const fetchEventData = async () => {
           try{
                  const response = await axios.get(api + `/api/event/${id}`)
                  console.log(response.data);
                  setEventData(response.data)
              }
                  catch(e : any){
                  setError(e.message);
                  toast.error("Wystąpił błąd przy pobieraniu danych wydarzenia.");
              }
          }
      useEffect(()=>{
          fetchEventData()
      },[])
  
      if (!eventData) {
          return <div className="spinner-border text-primary" role="status"></div>; 
        }

  const ticketPrices: { [key: string]: number } = {
    normalny: eventData.normalPrice, 
    vip: eventData.vipPrice,     
  };

  const handleQuantityChange = (type: string, value: number) => {
    setQuantities((prev) => ({
      ...prev,
      [type]: value,
    }));
  };

  const calculateTotal = () => {
    return Object.entries(quantities).reduce(
      (total, [type, quantity]) => total + ticketPrices[type] * quantity,
      0
    );
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setSuccess(false);

    try {
      const requests = Object.entries(quantities).flatMap(([type, quantity]) =>
        Array.from({ length: quantity }, () => {
          const data = { type, price: ticketPrices[type], eventId: Number(id), owner };
          console.log(data)
          return axios.post(api + "/api/ticket", data);
        })
      );
      await Promise.all(requests);
      setSuccess(true);
      toast.success("Bilety zostały utworzone pomyślnie!");
    } catch (e:any) {
      if (axios.isAxiosError(e) && e.response?.status === 401) {
        navigate("/login")
        toast.warning("Zaloguj się")
    } else {
        setError(e.message);
    }
      setError("Nie udało się utworzyć biletów. Spróbuj ponownie.");
      toast.error("Nie udało się utworzyć biletów. Spróbuj ponownie.");
    } finally {
      setLoading(false);
    }
  };

  return (
      <div className="container mt-5 mb-5 d-flex">
      <form onSubmit={handleSubmit} className="col-md-8" style={{ flex: 2 }}>
        <div className="mb-4">
          <h4>Normalny - {ticketPrices.normalny} PLN</h4>
          <div className="mb-3">
            <label htmlFor="quantity-normalny" className="form-label">Ilość Biletów</label>
            <input
              type="number"
              id="quantity-normalny"
              className="form-control"
              value={quantities.normalny}
              onChange={(e) => handleQuantityChange("normalny", Number(e.target.value))}
              min="0"
              required
            />
          </div>
        </div>
    
        <div className="mb-4">
          <h4>VIP - {ticketPrices.vip} PLN</h4>
          <div className="mb-3">
            <label htmlFor="quantity-vip" className="form-label">Ilość Biletów</label>
            <input
              type="number"
              id="quantity-vip"
              className="form-control"
              value={quantities.vip}
              onChange={(e) => handleQuantityChange("vip", Number(e.target.value))}
              min="0"
              required
            />
          </div>
        </div>
    
        <div className="mb-3">
          <h5>Całkowita Suma: {calculateTotal()} PLN</h5>
        </div>
    
        <button type="submit" className="btn btn-primary" disabled={loading}>
          {loading ? "Tworzenie..." : "Kup bilet"}
        </button>
      </form>
    
      <div className="col-md-4 d-flex align-items-center justify-content-center" style={{ flex: 1 }}>
        {eventData && (
          <div className="card" style={{ width: '100%', maxWidth: '300px' }}>
            <div className="card-body">
              <h5 className="card-title">{eventData.name}</h5>
              <h6 className="card-subtitle mb-2 text-muted">{eventData.localization}</h6>
              <p className="card-text">Data: {new Date(eventData.date).toLocaleDateString()}</p>
              <Link to={`/event/${id}`} className="btn btn-info">Zobacz szczegóły wydarzenia</Link>
            </div>
          </div>
        )}
      </div>
    </div>
    
    
    
  );
};

export default BuyTicket