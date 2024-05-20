import React from 'react';
import { NavLink, Outlet, useNavigate } from 'react-router-dom';
import { useAuth } from './Hooks/useAuth';

export const Navigation = () => {
  const { userSession, logout } = useAuth();
  const navigate = useNavigate();

  const onLogout = () => {
    logout();
    navigate('/');
  };

  return (
    <>
      <nav className="navbar navbar-expand-lg navbar-light">
        <div className="container-fluid">
          <div className="navbar-brand d-flex align-items-center">
            {/* <img src={logo} alt="Logo" style={{ maxWidth: "60px", height: "auto" }} className="me-2" /> */}
            <div>
              <p className="fs-4 mb-0">Quotations</p>
              <p className="fs-6 text-nowrap"><em>Estimate and Start Working</em></p>
            </div>
          </div>
          <button className="navbar-toggler" type="button" data-bs-toggle="collapse" 
          data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" 
          aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse justify-content-center" id="navbarNav">
            <ul className="navbar-nav">

              {userSession && userSession.role === "admin" && (
                <>
                  <li className="nav-item">
                    <NavLink to="/users" className="nav-link">
                      Users
                    </NavLink>
                  </li>
                  <li className="nav-item">
                    <NavLink to="/companies" className="nav-link">
                      Companies
                    </NavLink>
                  </li>
                  <li className="nav-item">
                    <NavLink to="/company-users" className="nav-link">
                      Company Users
                    </NavLink>
                  </li>
                </>
              )}

              {userSession && userSession.role === "user" && (
                <>
                  <li className="nav-item">
                    <NavLink to="/dashboard" className="nav-link">
                      Dashboard
                    </NavLink>
                  </li>
                  <li className="nav-item">
                    <NavLink to="/customers" className="nav-link">
                      Customers
                    </NavLink>
                  </li>
                  <li className="nav-item">
                    <NavLink to="/quotes" className="nav-link">
                      Quotes
                    </NavLink>
                  </li>
                </>)}

            </ul>
          </div>
          <div className="text-end">
            <p className="fs-6 mb-1 mt-0">{userSession ? userSession.email : ""}</p>
            <button className="btn btn-outline-primary btn-sm mb-0" onClick={onLogout}>Logout</button>
          </div>
        </div>
      </nav>
      <Outlet />
    </>
  );
};
