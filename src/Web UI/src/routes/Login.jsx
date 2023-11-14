import { useRef, useState, useEffect } from 'react';
import { useNavigate } from 'react-router';
import { useDispatch } from 'react-redux';
import { setCredentials } from '../auth/authSlice';
import { useLoginMutation } from '../auth/authApiSlice';

const Login = () => {
    const userNameRef = useRef();
    const errRef = useRef();
    const [userName, setUserName] = useState('');
    const [pwd, setPwd] = useState('');
    const [errMsg, setErrMsg] = useState('');
    const navigate = useNavigate();

    const [login, { isLoading }] = useLoginMutation();
    const dispatch = useDispatch();

    useEffect(() => {
        userNameRef.current.focus();
    }, []);

    useEffect(() => {
        setErrMsg('');
    }, [userName, pwd]);

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const userData = await login({ userName, password: pwd }).unwrap();
            dispatch(setCredentials({ ...userData, userName }))
            setUserName('');
            setPwd('');
            navigate('/profile');
        } catch (err) {
            if (!err?.response) {
                setErrMsg('No Server Response');
            } else if (err.response?.status === 400) {
                setErrMsg('Missing Username or Password');
            } else if (err.response?.status === 401) {
                setErrMsg('Unauthorized');
            } else {
                setErrMsg('Login Failed');
            }

            errRef.current.focus();
        }
    }

    const handleUserNameInput = (e) => setUserName(e.target.value);
    const handlePwdInput = (e) => setPwd(e.target.value);

    const content = isLoading ? <h1>Loading...</h1> : (
        <>
            <p ref={errRef} className={errMsg ? "block" : "hidden"}>{errMsg}</p>
            <h1>Login</h1>
            <form onSubmit={handleSubmit}>
                <label htmlFor='username'>Username:</label>
                <input 
                    type='text'
                    id='username'
                    ref={userNameRef}
                    value={userName}
                    onChange={handleUserNameInput}
                    required
                />
                <label htmlFor='password'>Password:</label>
                <input 
                    type='password'
                    id='password'
                    onChange={handlePwdInput}
                    value={pwd}
                    required
                />
                <button>Sign In</button>
            </form>
        </>
    );

    return content;
}

export default Login