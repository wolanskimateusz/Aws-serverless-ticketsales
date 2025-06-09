import axios from "axios";
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

type Artist = {
    name: string;
};

type CreateEventDto = {
    name: string;
    descripton: string;
    date: string;
    localization: string;
    artists: string[]; // Zmienione na tablicę nazw artystów
    normalPrice: number;
    vipPrice: number;
};

function CreateEventPage() {

    const [formData, setFormData] = useState<CreateEventDto>({
        name: "",
        descripton: "",
        date: "",
        localization: "",
        artists: [], // Tablica przechowująca same nazwy artystów
        normalPrice: 0,
        vipPrice: 0,
    });

    const [availableArtists, setAvailableArtists] = useState<string[]>([]); // Tablica nazw artystów
    const [selectedArtistName, setSelectedArtistName] = useState<string | null>(null);
    const navigate = useNavigate()

    const api = "https://hhsakppg0d.execute-api.eu-central-1.amazonaws.com"

    useEffect(() => {
        const fetchArtists = async () => {
            try {
                const response = await fetch(api + "/api/artist");
                if (!response.ok) {
                    throw new Error("Failed to fetch artists");
                }
                const data: Artist[] = await response.json();
                const artistNames = data.map(artist => artist.name); // Pobranie tylko nazw artystów
                setAvailableArtists(artistNames);
            } catch (error) {
                console.error("Error fetching artists:", error);
            }
        };

        fetchArtists();
    }, []);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleAddArtist = () => {
        if (selectedArtistName !== null && !formData.artists.includes(selectedArtistName)) {
            setFormData({
                ...formData,
                artists: [...formData.artists, selectedArtistName],
            });
            setSelectedArtistName(null); 
        }
    };

    const handleRemoveArtist = (name: string) => {
        setFormData({
            ...formData,
            artists: formData.artists.filter((artistName) => artistName !== name), 
        });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        console.log("Form data before submit:", formData);
        try {
            const response = await axios.post(api + "/api/event", formData);
            toast.success(`Tworzenie wydarzenia ${response.data.name} pomyślne!`);
            console.log(response.data);
            setFormData({
                name: "",
                descripton: "",
                date: "",
                localization: "",
                artists: [],
                normalPrice: 0,
                vipPrice: 0,
            });
            setSelectedArtistName(null);
            return response.data;
        } catch (error: any) {
            if(error.response && error.response.status === 403){
                navigate("/")
                toast.warning("Nie masz uprawnień dla tej strony")
            }
            if(error.response && error.response.status === 401){
                navigate("/login")
                toast.warning("Zaloguj się")
            }
        }
    };

    return (
        <div className="container mt-4">
            <h2>Stwórz Wydarzenie</h2>
            <form onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label htmlFor="name" className="form-label">Nazwa Wydarzenia</label>
                    <input
                        type="text"
                        className="form-control"
                        id="name"
                        name="name"
                        value={formData.name}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="descripton" className="form-label">Opis</label>
                    <textarea
                        className="form-control"
                        id="descripton"
                        name="descripton"
                        value={formData.descripton}
                        onChange={handleInputChange}
                        required
                    ></textarea>
                </div>
                <div className="mb-3">
                    <label htmlFor="date" className="form-label">Data</label>
                    <input
                        type="datetime-local"
                        className="form-control"
                        id="date"
                        name="date"
                        value={formData.date}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="localization" className="form-label">Lokalizacja</label>
                    <input
                        type="text"
                        className="form-control"
                        id="localization"
                        name="localization"
                        value={formData.localization}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="normalPrice" className="form-label">Cena normalnego biletu</label>
                    <input
                        type="number"
                        className="form-control"
                        id="normalPrice"
                        name="normalPrice"
                        value={formData.normalPrice}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label htmlFor="vipPrice" className="form-label">Cena biletu VIP</label>
                    <input
                        type="number"
                        className="form-control"
                        id="vipPrice"
                        name="vipPrice"
                        value={formData.vipPrice}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label className="form-label">Artyści</label>
                    <div className="input-group mb-3">
                        <select
                            className="form-select"
                            value={selectedArtistName || ""}
                            onChange={(e) => setSelectedArtistName(e.target.value || null)}
                        >
                            <option value="">Wybierz Artystów</option>
                            {availableArtists.map((artistName) => (
                                <option key={artistName} value={artistName}>
                                    {artistName}
                                </option>
                            ))}
                        </select>
                        <button
                            type="button"
                            className="btn btn-primary"
                            onClick={handleAddArtist}
                        >
                            Dodaj
                        </button>
                    </div>
                    <ul className="list-group">
                        {formData.artists.map((artistName) => (
                            <li key={artistName} className="list-group-item d-flex justify-content-between align-items-center">
                                {artistName}
                                <button
                                    type="button"
                                    className="btn btn-danger btn-sm"
                                    onClick={() => handleRemoveArtist(artistName)}
                                >
                                    Usuń
                                </button>
                            </li>
                        ))}
                    </ul>
                </div>
                <div className="d-flex justify-content-between m-2">
                    <button type="submit" className="btn btn-success">
                        Stwórz Wydarzenie
                    </button>
                    <Link to={`/artist/create`} className="btn btn-success">
                        Dodawanie artystów
                    </Link>
                </div>
            </form>
        </div>
    );
}

export default CreateEventPage;
