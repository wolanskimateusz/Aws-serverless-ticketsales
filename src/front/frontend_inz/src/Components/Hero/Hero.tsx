
import { ReactNode } from 'react'
import './Hero.css'
import HeroImage from './HeroImage.jpg'
import { Link } from 'react-router-dom';

interface Props {
    children?: ReactNode; 
  }

function Hero({children}: Props)
{
    return <div className='d-flex justify-content-center bg-dark py-3'>
        <div
      className="bg-image d-flex flex-column align-items-center justify-content-start pt-5"
      style={{
        backgroundImage: `url(${HeroImage})`,
        height: '100%',
        width:'90%',
        borderRadius: '25px',
        backgroundSize: 'cover',
        backgroundPosition: 'center'
      }} >
        <div className='text-white fw-bold  h-25 display-4 p-5 text-center'>
            Przeżyj niezapomniane emocje z twoimi ulubionymi artystami
         </div>
         <Link to={`/events`} className="btn btn-danger btn-lg m-5"
          style={{
            fontSize: '1.5rem', 
            padding: '20px 40px',
          }}>
          Przeglądaj wydarzenia
        </Link>
         <p className="text-white fs-2 text-decoration-underline mt-5">Nadchodzące wydarzenia</p>
         <div className=' d-flex flex-row  justify-content-center w-100'>
            {children}
        </div>
        </div>
        
    </div>
}

export default Hero