import { useNavigate } from "react-router";
import { Link } from "react-router-dom";
import { useForm } from "react-hook-form";
import { motion } from "framer-motion";
import { useRegisterMutation } from "../auth/authApiSlice";
import { useState } from "react";

const Register = () => {
    const {
        register,
        handleSubmit,
        formState: { errors },
        setError,
        getValues,
    } = useForm();

    const [apiError, setApiError] = useState("");
    const navigate = useNavigate();
    const [registerUser, { isLoading }] = useRegisterMutation();

    const onSubmit = async (data) => {
        try {
            await registerUser({
                username: data.username,
                password: data.password,
                email: data.email,
            }).unwrap();
            navigate("/login");
        } catch {
            if (!err?.response && err?.status === "FETCH_ERROR") {
                setApiError("Unable to reach server. Please try again later!");
            } else if (err.response?.status === 400) {
                setError("username", {
                    type: "manual",
                    message: "Username is required!",
                });
                setError("email", {
                    type: "manual",
                    message: "Email is required!",
                });
                setError("password", {
                    type: "manual",
                    message: "Password is required!",
                });
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
                        <h1>Create an account</h1>
                    </div>
                    {apiError && (
                        <motion.div
                            className="text-error mb-1"
                            initial={{ scale: 0.25, opacity: 0 }}
                            animate={{ scale: 1, opacity: 1 }}
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
                                autoComplete="off"
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
                                htmlFor="email"
                            >
                                Email:
                            </label>
                            <input
                                {...register("email", { required: true })}
                                className="input w-full"
                                type="email"
                                id="email"
                                autoComplete="off"
                                required
                            />
                            {errors.email && (
                                <p className="text-error mt-1">
                                    {errors.email.message}
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
                                autoComplete="off"
                                required
                            />
                            {errors.password && (
                                <p className="text-error mt-1">
                                    {errors.password.message}
                                </p>
                            )}
                        </div>
                        <div>
                            <label
                                className="block mb-2 text-sm font-medium"
                                htmlFor="confirmPassword"
                            >
                                Confirm password:
                            </label>
                            <input
                                {...register("confirmPassword", {
                                    required: true,
                                    validate: (val) =>
                                        val === getValues("password"),
                                })}
                                className="input w-full mb-1"
                                type="password"
                                id="confirmPassword"
                                autoComplete="off"
                                required
                            />
                            {errors.confirmPassword &&
                                errors.confirmPassword.type === "validate" && (
                                    <p className="text-error mt-1">
                                        Passwords do not match!
                                    </p>
                                )}
                        </div>
                        <button
                            className="btn btn-secondary text-neutral w-full disabled:opacity-75 disabled:bg-secondary disabled:text-neutral"
                            type="submit"
                            disabled={isLoading}
                        >
                            Register{" "}
                            {isLoading && (
                                <span className="loading loading-spinner loading-sm"></span>
                            )}
                        </button>
                        <p className="text-sm font-light">
                            Already have an account?{" "}
                            <Link
                                className="font-medium text-secondary hover:underline"
                                to="/login"
                            >
                                Sign in
                            </Link>
                        </p>
                    </form>
                </div>
            </div>
        </div>
    );

    return content;
};

export default Register;
