import { Link } from "react-router-dom"

interface Props{
  id: number
  name : string
  days_untill: number
  description: string
  localization: string
}

function Card({name, days_untill, description, localization, id}: Props)
{
    return <>
    <div className="card m-5 ">
  <div className="card-body">
    <h5 className="card-title p-1">{name}</h5>
    <h6 className="card-subtitle mb-2 text-muted p-1" >Wydarzenie za {days_untill} dni</h6>
    <p className="card-text p-1" >{description} <br /> {localization}</p>
    <Link to={`/event/${id}`} className="btn btn-danger">Zobacz wydarzenie </Link>
  </div>
</div>
    
</>
}

export default Card