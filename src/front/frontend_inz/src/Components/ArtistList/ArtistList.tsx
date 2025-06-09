import axios from "axios"
import { useEffect, useState } from "react"
import { Link, useNavigate } from "react-router-dom"

interface DataType{
    id: number,
    name: string,
    genre: string
}

function ArtistList(){

    const [data, setData] = useState<DataType[]|null>(null)
    const [error, setError] = useState<String|null> (null)

    const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"

    const fetchArtist = async () =>{
        try{
            const response = await axios.get(api  + "/api/artist")
            setData(response.data)
        }
        catch(e : any){
            setError(e.message)
        }
    }

    useEffect(()=>{
        fetchArtist()
    },[] )

    const navigate = useNavigate()

    return (
        <div className="bg-dark py-3">
          {error && <div className="alert alert-danger">{error}</div>}
          {data === null ? (
            <p>Loading...</p>
          ) : (
            <ul className="list-group">
              {data.map((artist) => (
                <li
                  className="list-group-item list-group-item-action hover"
                  key={artist.id}
                  onClick={() => navigate(`/artist/id/${artist.id}`)} // Cały prostokąt przekierowuje do szczegółów artysty
                  style={{ cursor: "pointer" }} // Zmieniamy wskaźnik kursora
                >
                  <div >
                    <h5>{artist.name}</h5>
                    <Link
                      to={`/artist/genre/${artist.genre}`}
                      className="text-decoration-none text-primary"
                      onClick={(e) => e.stopPropagation()} // Zatrzymujemy propagację, aby kliknięcie nie wywołało nawigacji rodzica
                    >
                      {artist.genre}
                    </Link>
                  </div>
                </li>
              ))}
            </ul>
          )}
        </div>
      );
      
      
}

export default ArtistList