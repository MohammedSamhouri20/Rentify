import "bootstrap/dist/css/bootstrap.rtl.min.css";
import "../node_modules/bootstrap/dist/css/bootstrap.rtl.min.css";
import "@/styles/css/main.rtl.min.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import NavigationBar from "@/components/NavigationBar";
import Footer from "@/components/Footer";
import HomePage from "@/views/HomePage";
import ResultsPage from "@/views/ResultsPage";
import ItemDetailsPage from "@/views/ItemDetailsPage";
import UserPage from "@/views/UserPage";
import AddItemPage from "@/views/AddItemPage";
import UpdateItemPage from "@/views/UpdateItemPage";
import Container from "react-bootstrap/Container";
import Col from "react-bootstrap/Col";
import ProfilePage from "@/views/ProfilePage";
import ChatPage from "@/views/ChatPage";
import LoginPage from "@/views/LoginPage";
import SignupPage from "@/views/SignupPage";
import BookingManagementPage from "@/views/BookingManagementPage";
import { AuthProvider } from "@/context/AuthContext";
import CompleteBookingPage from "@/views/CompleteBookingPage";
import ScrollToTop from "@/utils/ScrollToTop";
function App() {
  return (
    <>
      <AuthProvider>
        <BrowserRouter>
          <ScrollToTop />
          <NavigationBar />
          <Container fluid>
            <Col>
              <Routes>
                <Route index element={<HomePage />} />
                <Route path="/Home" element={<HomePage />} />
                <Route path="/About-Rentify" element={<HomePage />} />
                <Route path="/Results" element={<ResultsPage />} />
                <Route path="/product/:id" element={<ItemDetailsPage />} />
                <Route path="/service/:id" element={<ItemDetailsPage />} />
                {/* <Route path="/user/:id" element={<UserPage />} /> */}
                <Route
                  path="/booking-management/:itemType/:userRole/:bookingId"
                  element={<BookingManagementPage />}
                />
                <Route path="/ProfilePage" element={<ProfilePage />} />
                <Route path="/ChatPage" element={<ChatPage />} />
                <Route
                  path="/CompleteBookingPage/:itemType/:bookingId"
                  element={<CompleteBookingPage />}
                />
                <Route
                  path="/update/:itemType/:id"
                  element={<UpdateItemPage />}
                />

                <Route path="/AddItem" element={<AddItemPage />} />

                <Route path="/Login" element={<LoginPage />} />
                <Route path="/SignUp" element={<SignupPage />} />
              </Routes>
            </Col>
          </Container>
          <Footer />
        </BrowserRouter>
      </AuthProvider>
    </>
  );
}

export default App;
