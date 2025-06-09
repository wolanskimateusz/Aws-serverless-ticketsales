import axios from "axios"
import { useEffect, useState } from "react"
import { Link } from "react-router-dom"
import { toast } from "react-toastify"

interface EventList {
    id: number
    name: string
}

function SearchBar(){

    const [searchQuery, setSearchQuery] = useState<string>('')
    const [result, setResult] = useState<EventList[]|null>(null)

    const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"

    useEffect(() =>{
        const fetchData = async () => {

            if (searchQuery.trim() === '') {
                setResult(null)
                return
            }
            try{
                const response = await axios.get(api + `/api/event?EventName=${searchQuery}`)
                setResult(response.data)
            }
            catch(e: any)
            {
                toast.error("Wystąpił błąd przy pobieraniu danych wydarzenia.");
            }
        }
        fetchData()
    },[searchQuery])
   

    const handleEventClick = () => {
        setSearchQuery(''); 
    }

    return (
        <div>
           
            <div className="input-group" > 
                <input
                    type="text"
                    className="form-control"
                    placeholder="Wyszukaj wydarzenia..."
                    aria-label="Search events"
                    value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)} 
                />
            </div>

      
            {result && result.length > 0 && (
                <div 
                    style={{ 
                        position: 'absolute',  
                        zIndex: 9999, 
                        backgroundColor: 'white',  
                        width: '40%',  
                        boxShadow: '0px 4px 6px rgba(0, 0, 0, 0.1)',  
                    }}
                >
                    {result.map((event) => (
                        <Link
                            key={event.id} 
                            to={`/event/${event.id}`}  
                            className="text-decoration-none" 
                            style={{ display: 'block', padding: '10px', borderBottom: '1px solid #ccc' }} 
                            onClick={handleEventClick} 
                        >
                            <div>
                                <strong>{event.name}</strong>
                            </div>
                        </Link>
                    ))}
                </div>
            )}

        </div>
    );
}

export default SearchBar