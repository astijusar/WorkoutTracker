import { useNavigate } from "react-router";
import { useDispatch } from "react-redux";
import { useForm } from "react-hook-form";
import { motion } from "framer-motion";
import { setCredentials } from "../auth/authSlice";
import { useLoginMutation } from "../auth/authApiSlice";
import { useState } from "react";

const Login = () => {
    const {
        register,
        handleSubmit,
        formState: { errors },
        setError,
    } = useForm();

    const [apiError, setApiError] = useState("");
    const navigate = useNavigate();
    const [login, { isLoading }] = useLoginMutation();
    const dispatch = useDispatch();

    const onSubmit = async (data) => {
        try {
            const userData = await login({
                username: data.username,
                password: data.password,
            }).unwrap();
            dispatch(setCredentials({ ...userData, userName }));
            navigate("/profile");
        } catch (err) {
            console.log(err);
            if (!err?.response && err?.status === "FETCH_ERROR") {
                setApiError("Unable to reach server. Please try again later!");
            } else if (err.response?.status === 400) {
                setError("username", {
                    type: "manual",
                    message: "Username is required!",
                });
                setError("password", {
                    type: "manual",
                    message: "Password is required!",
                });
            } else if (err.response?.status === 401) {
                setApiError("Username or password is incorrect!");
            } else {
                setApiError("Please try again later!");
            }
        }
    };

    const content = (
        <div className="w-full min-h-screen flex justify-center items-center">
            <div className="card mx-3 sm:mx-none bg-neutral md:mx-auto md:w-3/5 lg:w-2/4 xl:w-1/3">
                <div className="card-body shadow-xl">
                    <div className="card-title mb-2 text-3xl font-bold leading-tight tracking-tight">
                        <h1>Sign in to your account</h1>
                    </div>
                    {apiError && (
                        <motion.div
                            className="text-error mb-1"
                            initial={{ scale: 0.5, opacity: 0 }}
                            animate={{ scale: 1, opacity: 1 }}
                            exit={{ scale: 0.5, opacity: 0 }}
                            transition={{ duration: 0.5 }}
                        >
                            {apiError}
                        </motion.div>
                    )}
                    <form
                        className="space-y-4 md:space-y-6"
                        onSubmit={handleSubmit(onSubmit)}
                    >
                        <div>
                            <label
                                className="block mb-2 text-sm font-medium"
                                htmlFor="username"
                            >
                                Username:
                            </label>
                            <input
                                {...register("username", { required: true })}
                                className="input w-full"
                                type="text"
                                id="username"
                                required
                            />
                            {errors.username && (
                                <p className="text-error mt-1">
                                    {errors.username.message}
                                </p>
                            )}
                        </div>
                        <div>
                            <label
                                className="block mb-2 text-sm font-medium"
                                htmlFor="password"
                            >
                                Password:
                            </label>
                            <input
                                {...register("password", {
                                    required: true,
                                })}
                                className="input w-full mb-1"
                                type="password"
                                id="password"
                                required
                            />
                            {errors.password && (
                                <p className="text-error mt-1">
                                    {errors.password.message}
                                </p>
                            )}
                        </div>
                        <button
                            className="btn btn-secondary text-neutral w-full disabled:opacity-75 disabled:bg-secondary disabled:text-neutral"
                            type="submit"
                            disabled={isLoading}
                        >
                            Sign In{" "}
                            {isLoading && (
                                <span className="loading loading-spinner loading-sm"></span>
                            )}
                        </button>
                        <p className="text-sm font-light">
                            Don’t have an account yet?{" "}
                            <a
                                href="#"
                                className="font-medium text-secondary hover:underline "
                            >
                                Sign up
                            </a>
                        </p>
                    </form>
                </div>
            </div>
        </div>
    );

    return content;
};

export default Login;