import axios from "axios";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

interface ArtistData{
    name: string
    genre: string
}
interface Genres{
    name: string
}


function ArtistCreatePage(){
    const [artist, setArtist] = useState<ArtistData>({ name: '', genre: '' });
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<boolean>(false);
    const [genres, setGenres] = useState<Genres[]>([]);
    const [newGenre, setNewGenre] = useState<string>('');

    const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"

    
    const fetchGenres = async () => {
      try {
        const response = await axios.get(api + '/api/artist/genres');
        setGenres(response.data);
        
      } catch (err) {
        toast.error("Nie udało się pobrać listy gatunków.");
      }
    };
  
    useEffect(() => {
      fetchGenres();
    }, []);
  
    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
      const { name, value } = e.target;
      setArtist((prev) => ({
        ...prev,
        [name]: value,
      }));
    };
  
    const handleGenreChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
      const selectedGenre = e.target.value;
      setArtist((prev) => ({
        ...prev,
        genre: selectedGenre,
      }));
    };
  
    const handleNewGenreChange = (e: React.ChangeEvent<HTMLInputElement>) => {
      setNewGenre(e.target.value);
    };
  
    const handleSubmit = async (e: React.FormEvent) => {
      e.preventDefault();
      setLoading(true);
      setError(null);
      setSuccess(false);
  
    
      const genreToSubmit = newGenre ? newGenre : artist.genre;
  
      try {
        const response = await axios.post('https://localhost:7109/api/artist', { ...artist, genre: genreToSubmit });
        setSuccess(true);
        toast.success("Artysta został utworzony!");
      } catch (err) {
        setError('Nie udało się utworzyć artysty. Spróbuj ponownie.');
        toast.error("Nie udało się utworzyć artysty. Spróbuj ponownie.");
      } finally {
        setLoading(false);
      }
    };
  
    return (
      <div className="container m-5">
        <h2 className="mb-4">Tworzenie Artysty</h2>
  
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label htmlFor="name" className="form-label">Nazwa Artysty</label>
            <input
              type="text"
              className="form-control"
              id="name"
              name="name"
              value={artist.name}
              onChange={handleChange}
              required
            />
          </div>
  
          <div className="mb-3">
            <label htmlFor="genre" className="form-label">Gatunek</label>
            <select
              className="form-select"
              id="genre"
              name="genre"
              value={artist.genre}
              onChange={handleGenreChange}
              required
            >
              <option value="">Wybierz gatunek</option>
              {genres.map((genre) => (
                <option key={genre.name} value={genre.name}>
                  {genre.name}
                </option>
              ))}
            </select>
  
            <div className="mt-2">
              <label htmlFor="new-genre" className="form-label">Dodaj nowy gatunek</label>
              <input
                type="text"
                id="new-genre"
                className="form-control"
                value={newGenre}
                onChange={handleNewGenreChange}
                placeholder="Wpisz nowy gatunek"
              />
            </div>
          </div>
  
          <button type="submit" className="btn btn-primary" disabled={loading}>
            {loading ? 'Tworzenie...' : 'Utwórz Artystę'}
          </button>
        </form>
  
      </div>
    );
}
export default ArtistCreatePage