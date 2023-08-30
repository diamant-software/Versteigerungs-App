import { useMsal } from '@azure/msal-react';

const LogoutButton: React.FC<{}> = () => {
  const { instance } = useMsal();

  const handleLogin = () => {
    instance.logoutRedirect().catch((e) => {
      console.error(`logoutRedirect failed: ${e}`);
    });
  };

  return (
    <button className="mt-2 rounded bg-blue-500 px-4 py-2 text-gray-100" onClick={handleLogin}>
      Logout
    </button>
  );
};

export { LogoutButton };
