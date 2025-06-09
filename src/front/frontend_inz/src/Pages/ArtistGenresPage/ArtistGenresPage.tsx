import { useParams } from "react-router-dom"
import ArtistByGenreList from "../../Components/ArtistByGenreList/ArtistByGenreList";


function ArtistsGenresPage(){

    const {genre} = useParams<{ genre: string }>();

    return <>

    <ArtistByGenreList genre={genre || ''} />
    
    </>
}
export default ArtistsGenresPage