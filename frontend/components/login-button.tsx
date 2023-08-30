import { loginRequest } from "@/app/authConfig";
import { useMsal } from "@azure/msal-react";

const LoginButton: React.FC<{}> = () => {
    const { instance } = useMsal();

    const handleLogin = () => {
        instance.loginRedirect(loginRequest).catch((e) => { console.error(`loginRedirect failed: ${e}`) })
    }

    return (
        <button className="mt-2 rounded bg-blue-500 px-4 py-2" onClick={handleLogin}>Login</button>
    )
}

export { LoginButton };