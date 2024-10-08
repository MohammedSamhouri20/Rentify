import React from "react";
import { useState, useEffect } from "react";
import { Nav, Navbar, Button, Image, Container } from "react-bootstrap";
import LogoImage from "@/assets/images/RentifyLogo.png";
import "./styles.css";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "@/context/AuthContext";

export default function NavigationBar() {
  const { auth, logout } = useAuth();
  const navigate = useNavigate();
  const [avatar, setAvatar] = useState(null);

  useEffect(() => {
    if (auth.isAuthenticated) {
      fetchUserAvatar();
    }
  }, [auth.isAuthenticated]);

  const fetchUserAvatar = async () => {
    try {
      const response = await axios.get(
        "http://localhost:5079/api/account/data",
        {
          headers: {
            Authorization: `Bearer ${auth.token}`,
          },
        }
      );

      console.log();

      setAvatar(response.data.userData.avatar);
    } catch (error) {
      console.error("Failed to fetch avatar:", error);
    }
  };

  const handleLogout = () => {
    logout();
    navigate("/Home");
  };

  return (
    <Navbar expand="lg">
      <Navbar.Brand
        as={Link}
        to="/Home"
        style={{ minWidth: "150px", width: "150px" }}
      >
        <Image fluid className="brand-logo" src={LogoImage} alt="Logo" />
      </Navbar.Brand>

      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Container
          fluid
          className="d-flex text-nowrap flex-lg-row flex-column gap-3 text-center justify-content-between nav-items-container"
        >
          <Nav className="gap-2 fs-6">
            <Nav.Link as={Link} to="/Home">
              الصفحة الرئيسية
            </Nav.Link>
            <Nav.Link href="/Home#how-it-works">كيف يعمل الموقع</Nav.Link>
          </Nav>

          <Nav className="gap-2 col-lg-auto col-12 col-sm-10 m-lg-0 m-auto">
            <Button
              onClick={() => navigate("/AddItem")}
              size="sm"
              variant="primary"
              className="text-nowrap text-white"
            >
              انشر إعلان
            </Button>

            {auth.isAuthenticated ? (
              <>
                <Button
                  onClick={handleLogout}
                  size="sm"
                  variant="outline-secondary"
                  className="text-nowrap"
                >
                  تسجيل الخروج
                </Button>
                <Image
                  src={avatar}
                  alt="Profile"
                  roundedCircle
                  style={{ width: "40px", height: "40px", cursor: "pointer" }}
                  onClick={() => navigate("/ProfilePage")}
                />
              </>
            ) : (
              <>
                <Button
                  onClick={() => navigate("/SignUp")}
                  size="sm"
                  variant="secondary"
                  className="text-nowrap"
                >
                  إنشاء حساب
                </Button>
                <Button
                  onClick={() => navigate("/Login")}
                  size="sm"
                  variant="outline-secondary"
                  className="text-nowrap"
                >
                  تسجيل الدخول
                </Button>
              </>
            )}
          </Nav>
        </Container>
      </Navbar.Collapse>
    </Navbar>
  );
}
