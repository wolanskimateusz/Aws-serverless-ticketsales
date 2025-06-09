import { useEffect, useState } from "react";
import { useAuth } from "../../Context/useAuth"
import axios from "axios";
import { Link } from "react-router-dom";

interface UserData {
    tickets: { id: number; eventName: string }[];
    email: string
    Id: number
}


function ProfilePage()
{
    const {user} = useAuth()

    const [data, setData] = useState<UserData| null>(null);
    const [error, setError] = useState<String|null> (null)

    const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"

    const fetchUserData = async () => {
     try{
            const response = await axios.get(api + `/api/account/user?name=${user?.userName}`)
            console.log(response.data);
            setData(response.data)
        }
            catch(e : any){
            setError(e.message)
        }
    }

    useEffect(()=>{
            fetchUserData()
        },[] )


        return (
            <div className="container m-5">
              <div className="card shadow-sm">
                <div className="card-body">
                  <h1 className="card-title">Profil użytkownika</h1>
                  <p>
                    <strong>Nazwa użytkownika:</strong> {user?.userName}
                  </p>
                  <p>
                    <strong>Email:</strong> {data?.email}
                  </p>
                  <h2 className="mt-4">Lista biletów:</h2>
                  {data?.tickets.length ? (
                    <ul className="list-group">
                      {data.tickets.map((ticket) => (
                        <li
                          className="list-group-item d-flex justify-content-between align-items-center"
                          key={ticket.id}
                        >
                          <span>{ticket.eventName}</span>
                          <Link
                            to={`/ticket/${ticket.id}`} 
                            className="btn btn-primary btn-sm"
                          >
                            Szczegóły
                          </Link>
                        </li>
                      ))}
                    </ul>
                  ) : (
                    <p className="text-muted">Brak biletów do wyświetlenia.</p>
                  )}
                </div>
              </div>
            </div>
          );
        }

export default ProfilePage