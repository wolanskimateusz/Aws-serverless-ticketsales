import axios from "axios"
import { useEffect, useState } from "react"
import { Link } from "react-router-dom"
import { v4 as uuidv4 } from "uuid";

interface DataType{
    name: string
}

function GenreList(){

    const [data, setData] = useState<DataType[]|null>(null)
    const [error, setError] = useState<String[]|null>(null)

    const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"

    const fetchGenres = async () => {
        try{
            const response = await axios.get(api + "/api/artist/genres")
            setData(response.data)
        }
        catch(e:any){
            setError(e.message)
        }

    }

    useEffect(() => {
        fetchGenres()
    },[])


    return <div className=" bg-dark py-3">
    {error && <div className="alert alert-danger">{error}</div>}
    {data === null ? (
            <p>Loading...</p>
        ) : (
            <ul className="list-group">
                {data.map((genre) => (
                <Link to={`/artist/genre/${genre.name}`} className="text-decoration-none text-white" key={genre.name}>
                    <li  className="list-group-item list-group-item-action hover">
                        <h5>{genre.name}</h5>
                    </li>
                </Link>
                ))}
            </ul>
        )}
    
</div>
}

export default GenreList