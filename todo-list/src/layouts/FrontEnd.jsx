import { useEffect, useState } from "react";
import Navbar from "../components/Navbar";

const FrontEnd = ({ children }) => {
  const [accessToken, setAccessToken] = useState(null);

  const logOut = () => {
    localStorage.clear();
    window.location.replace("/"); 
  };

  useEffect(() => {
    const tmp = localStorage.getItem("accessToken");
    if (tmp) {
      setAccessToken(tmp);
    }
  }, []);

  return (
    <>
      <Navbar accessToken={accessToken} logOut={logOut} />
      <main>{children}</main>
    </>
  );
};

export default FrontEnd;