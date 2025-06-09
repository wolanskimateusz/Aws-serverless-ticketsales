import * as Yup from "yup"
import { useAuth } from "../../Context/useAuth"
import { yupResolver } from "@hookform/resolvers/yup"
import { useForm } from "react-hook-form"

type RegisterFormInputs = {
    username: string
    email: string
    password: string
}

const validation = Yup.object().shape({
  email: Yup.string().required("Email nie może być pusty"),
  username: Yup.string().required("Nazwa nie może być pusta"),
  password: Yup.string().required("Hasło nie może być puste")
})

function Register()
{
   const {registerUser} = useAuth();
     const {register, handleSubmit, formState:{errors}} = useForm<RegisterFormInputs>({resolver: yupResolver(validation)})
   
     const handleRegister = (form: RegisterFormInputs) => {
        registerUser(form.email, form.username, form.password)
     }


  return <section className="bg-light">
  <div className="d-flex flex-column align-items-center justify-content-center p-6 mx-auto vh-100">
    <div className="w-50 bg-white rounded-lg shadow border mb-5 bg-white p-4">
      <div className="p-6 space-y-4 sm:p-8">
        <h1 className="h3 font-weight-bold text-dark">
          Rejestracja
        </h1>
        <form className="space-y-4" onSubmit={handleSubmit(handleRegister)}>
        <div>
            <label htmlFor="email" className="form-label text-dark"> Email</label>
            <input type="text" id="email" className="form-control"placeholder="Email"
              {...register("email")} />
              {errors.email ? <p>{errors.email.message}</p>: ""}
          </div>
          <div>
            <label htmlFor="username" className="form-label text-dark"> Nazwa użytkownika</label>
            <input type="text" id="username" className="form-control"placeholder="Nazwa użytkownika"
              {...register("username")} />
              {errors.username ? <p>{errors.username.message}</p>: ""}
          </div>
          <div>
            <label htmlFor="password" className="form-label text-dark"> Hasło </label>
            <input type="password" id="password" placeholder="••••••••" className="form-control"
              {...register("password")}/>
              {errors.password ? <p>{errors.password.message}</p>: ""}
          </div>
          <button type="submit" className="btn btn-primary w-100 mt-3" >
            Zarejestruj się
          </button>
        </form>
      </div>
    </div>
  </div>
</section>
}

export default Register