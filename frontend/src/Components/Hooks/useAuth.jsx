import React, { createContext, useContext, useMemo, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { useLocalStorage } from "./useLocalStorage";

const AuthContext = createContext(undefined);

export const AuthProvider = ({ children }) => {
  const [userSession, setUserSession] = useLocalStorage("userSession", null);
  const navigate = useNavigate();

  const login = useCallback(async (data) => {
    setUserSession({ email: data.email, role: data.role });
    if (data.role === 'user') {
      navigate("/dashboard");
    } else if (data.role ==='admin'){
      navigate("/users");
    }
  }, [navigate, setUserSession]);

  const logout = useCallback(() => {
    setUserSession(null);
    navigate("/", { replace: true });
  }, [navigate, setUserSession]);

  const value = useMemo(
    () => ({
      userSession,
      login,
      logout,
    }),
    [userSession, login, logout]
  );

  return (
    <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("Error Logging in.");
  }
  return context;
};
