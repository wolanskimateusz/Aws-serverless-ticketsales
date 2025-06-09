import axios from "axios"
import { useEffect, useState } from "react"
import { Link } from "react-router-dom"
import { toast } from "react-toastify"


interface DataType {
    id: number,
    name: string,
    descripton: string,
    date: string
    localization: string
}

interface EventListProps {
    searchQuery: string;
}

function EventList({ searchQuery }: EventListProps) {

    const [data, setData] = useState<DataType[] | null>(null)
    const [error, setError] = useState<string | null>(null)
    const [currentPage, setCurrentPage] = useState<number>(1)
    const [eventsPerPage] = useState<number>(8)
    
    const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"

    useEffect(() => {
        
        const fetchEvents = async () => {
        try {
            const response = await axios.get(api + `/api/event?Date=${searchQuery}`)
            setData(response.data)
        } catch (e: any) {
            setError(e.message)
            toast.error("Wystąpił błąd przy pobieraniu danych wydarzenia.");
        }
    }
    
        fetchEvents()
    }, [searchQuery])

    const formatDate = (dateString: string): string => {
        const date = new Date(dateString);
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        const hours = String(date.getHours()).padStart(2, '0');
        const minutes = String(date.getMinutes()).padStart(2, '0');
        return `${year}-${month}-${day} ${hours}:${minutes}`;
    };

    // Get the events for the current page
    const indexOfLastEvent = currentPage * eventsPerPage;
    const indexOfFirstEvent = indexOfLastEvent - eventsPerPage;
    const currentEvents = data ? data.slice(indexOfFirstEvent, indexOfLastEvent) : [];

    // Handle page change
    const paginate = (pageNumber: number) => setCurrentPage(pageNumber);

    return (
        <div className="bg-dark py-3">
            {error && <div className="alert alert-danger">{error}</div>}
            {data === null ? (
                <p>Loading...</p>
            ) : (
                <>
                    <ul className="list-group">
                        {currentEvents.map((event) => (
                            <Link to={`/event/${event.id}`} key={event.id} className="text-decoration-none text-white">
                                <li className="list-group-item list-group-item-action hover">
                                    <h5>{event.name}</h5>
                                    <p>{event.descripton}</p>
                                    <small>{formatDate(event.date)}</small>
                                </li>
                            </Link>
                        ))}
                    </ul>
                    
                    {/* Paginacja */}
                    <div className="d-flex justify-content-center mt-4">
                        <button
                            className="btn btn-outline-light mr-2"
                            onClick={() => paginate(currentPage - 1)}
                            disabled={currentPage === 1}
                        >
                            Poprzednia
                        </button>
                        <button
                            className="btn btn-outline-light"
                            onClick={() => paginate(currentPage + 1)}
                            disabled={indexOfLastEvent >= (data?.length || 0)}
                        >
                            Następna
                        </button>
                    </div>
                </>
            )}
        </div>
    );
}

export default EventList;
