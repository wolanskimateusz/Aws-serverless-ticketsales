import axios from "axios";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";

interface TicketData{
    type: string
    price: number
    eventName: string
    eventId:number
}

function TicketDetails() {
   
const { id } = useParams<{ id: string }>(); 
    const [data, setData] = useState<TicketData>()
    const [error, setError] = useState<String|null> (null)

    const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"
    
    const fetchTicketData = async () => {
         try{
                const response = await axios.get(api + `/api/ticket/${id}`)
                console.log(response.data);
                setData(response.data)
            }
                catch(e : any){
                setError(e.message)
            }
        }
    useEffect(()=>{
        fetchTicketData()
    },[])

    if (!data) {
        return <div className="spinner-border text-primary" role="status"></div>; 
      }


  return (
    <div className="container m-5">
      <h1>Szczegóły biletu</h1>
      <div className="card shadow-sm">
        <div className="card-body">
          <h2 className="card-title">Bilet do wydarzenia: {data.eventName}</h2>
          <p><strong>Typ biletu:</strong> {data.type}</p>
          <p><strong>Cena:</strong> {data.price} PLN</p>

          {/* Link do strony wydarzenia */}
          <Link to={`/event/${data.eventId}`} className="btn btn-primary">
            Zobacz wydarzenie
          </Link>
        </div>
      </div>
    </div>
  );
  }

  export default TicketDetails