import axios from "axios";
import { useEffect, useState } from "react";
import { Link, useLocation, useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom"
import { toast } from "react-toastify";

interface EventData{
    name: string
    descripton: string
    date: string
    localization: string
    artists: {id: number, name: string}[]
}

function EventDetails(){

  const navigate = useNavigate()
    const { id } = useParams<{ id: string }>();  
    const location = useLocation();

    const [data, setData] = useState<EventData>()
    const [error, setError] = useState<String|null> (null)
    const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"
    
    const fetchEventData = async () => {
         try{
                const response = await axios.get(api + `/api/event/${id}`)
                console.log(response.data);
                setData(response.data)
            }
                catch(e : any){
                  if (e.response && e.response.status === 403) {
                    // Jeśli błąd to 403, przekierowujemy użytkownika na stronę logowania
                    navigate("/")
                    toast.warning("Nie masz uprawnień dla tej strony")
                } else {
                    // Jeśli błąd nie jest 403, ustawiamy komunikat o błędzie
                    setError(e.message);
                }
            }
        }
    useEffect(()=>{
        fetchEventData()
    },[location])

    if (!data) {
        return <div className="spinner-border text-primary" role="status"></div>; 
      }

      return (
        <div className="container m-5">
          <h1 className="mb-4">{data.name}</h1>
    
          <div className="card shadow-sm">
            <div className="card-body">
              <h5 className="card-title">Opis wydarzenia</h5>
              <p className="card-text">{data.descripton}</p>
    
              <div className="row">
                <div className="col-6 mb-4">
                  <p><strong>Data:</strong> {new Date(data.date).toLocaleString()}</p>
                  <p><strong>Lokalizacja:</strong> {data.localization}</p>
                </div>
                <div className="col-6">
                  <h6>Artyści:</h6>
                  <ul className="list-group">
                  {data.artists.map((artist) => (
                  <Link to={`/artist/id/${artist.id}`} key={artist.id} className="list-group-item list-group-item-action">
                    {artist.name}
                  </Link>
                ))}
                  </ul>
                </div>
              </div>
            </div>
            <Link to={`/buyTicket/${id}`} className="btn btn-primary position-absolute bottom-0 start-0 m-2">
          Kup Bilet
        </Link>
          </div>
        </div>
      );
}
export default EventDetails