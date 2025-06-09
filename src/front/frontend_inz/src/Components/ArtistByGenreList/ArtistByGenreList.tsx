import axios from "axios"
import { useEffect, useState } from "react"
import { Link } from "react-router-dom"

interface Props{
    id : number,
    genre : string,
    name : string
}

interface ArtistByGenreListProps {
    genre: string;
}

function ArtistByGenreList({genre} : ArtistByGenreListProps){

    const [data,setData] = useState<Props[] | null> (null)
    const [error,setError] = useState<String | null> (null)

    const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"

    const fetchArtistByGenre = async () => {
        try{
            const response = await axios.get(api + `/api/artist/genre/${genre}`)

            setData(response.data)
        }
        catch(e : any){
            setError(e.message)
        }
    }

    useEffect(()=>{
        fetchArtistByGenre()
    },[])


    return <div className=" bg-dark py-3">
    {error && <div className="alert alert-danger">{error}</div>}
    {data === null ? (
            <p>Loading...</p>
        ) : (
            <ul className="list-group">
                {data.map((artist) => (
                <Link to={`/artist/id/${artist.id}`} className="text-decoration-none text-white" key={artist.id}>
                    <li  className="list-group-item list-group-item-action hover">
                        <h5>{artist.name}</h5>
                    </li>
                </Link>
                ))}
            </ul>
        )}
    
</div>
}

export default ArtistByGenreList