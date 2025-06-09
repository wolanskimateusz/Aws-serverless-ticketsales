import axios from "axios";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";


interface Artist {
    name: string;
    genre: string;
  }
  
  interface Event {
    id: number;
    name: string;
  }

function ArtistDetails(){
    
    const { id } = useParams<{ id: string }>();
    const [artist, setArtist] = useState<Artist | null>(null);
    const [events, setEvents] = useState<Event[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    console.log(artist)

    const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"
  
    useEffect(() => {
      const fetchArtistDetails = async () => {
        try {
          setLoading(true);
        
          const artistResponse = await axios.get<Artist>(api + `/api/artist/${id}`);
          setArtist(artistResponse.data);
  
          const eventsResponse = await axios.get<Event[]>(api + `/api/event/eventList?artistId=${id}`);
          setEvents(eventsResponse.data);
        } catch (e) {
          setError('Nie udało się pobrać danych artysty.');
        } finally {
          setLoading(false);
        }
      };
  
      fetchArtistDetails();
    }, []);
  
    if (loading) return <p className="text-center mt-4">Ładowanie danych...</p>;
    if (error) return <p className="text-danger text-center mt-4">{error}</p>;
  
    return (
        <div className="container m-5">
          <div className="row">
            <div className="col-md-6 offset-md-5">
              <div className="card shadow">
                <div className="card-body">
                  <h3 className="card-title text-center">Szczegóły Artysty</h3>
                  {artist && (
                    <div className="text-center">
                      <p>
                        <strong>Nazwa:</strong> {artist.name}
                      </p>
                      <p>
                        <strong>Gatunek:</strong> {artist.genre}
                      </p>
                    </div>
                  )}
                </div>
              </div>
      
              <div className="m-4">
                <h4 className="text-center">Wydarzenia z udziałem artysty</h4>
                {events.length > 0 ? (
                  <ul className="list-group">
                    {events.map((event) => (
                      <li key={event.id} className="list-group-item">
                        <div className="d-flex justify-content-between align-items-center">
                          <Link
                            to={`/event/${event.id}`}
                            className="text-decoration-none w-100"
                          >
                            <div className="d-flex justify-content-between align-items-center w-100">
                              <div>
                                <strong>{event.name}</strong>
                              </div>
                            </div>
                          </Link>
                        </div>
                      </li>
                    ))}
                  </ul>
                ) : (
                  <p className="text-center mt-3">Brak zaplanowanych wydarzeń dla tego artysty.</p>
                )}
              </div>
            </div>
          </div>
        </div>
      );
}

export default ArtistDetails