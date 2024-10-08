import Container from "react-bootstrap/Container";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import Image from "react-bootstrap/Image";
import Button from "react-bootstrap/Button";
import { LinkContainer } from "react-router-bootstrap";
import EarnMoney from "@/assets/images/earnMoney.jpg";
import RentAnything from "@/assets/images/rentAnything.jpg";
import { Stack } from "react-bootstrap";
export default function CTASection() {
  return (
    <Container fluid className="p-4">
      <Row className="justify-content-sm-around justify-content-md-evenly gy-3 mb-4">
        <Col lg={4} md={5} sm={6} xs={{ order: "first", span: "12" }}>
          <Image fluid src={EarnMoney} rounded />
        </Col>
        <Col
          className="d-flex flex-column justify-content-center gap-5 align-items-start"
          md={5}
          sm={6}
          xs={12}
        >
          <h4>ابدأ رحلتك في الاستفادة من بضاعتك الكاسدة..</h4>
          <p className="fs-6">
            انشر إعلانك الآن واستفد من كل قطعة لديك! انضم إلى مجتمع Rentify
            وابدأ بتحقيق دخل إضافي من منتجاتك وخدماتك.
          </p>
          <LinkContainer to="/AddItem">
            <Button className="text-white" variant="primary">
              انشر ما لديك
            </Button>
          </LinkContainer>
        </Col>
      </Row>

      <Row className="justify-content-sm-around justify-content-md-evenly gy-3 mb-4">
        <Col
          className="d-flex flex-column justify-content-center gap-5 align-items-start"
          md={5}
          sm={6}
          xs={12}
        >
          <h4>استأجر ما تحتاج..</h4>
          <p className="fs-6">
            حياتك أسهل وأكثر متعة مع Rentify! استأجر ما تحتاج متى تحتاج، دون
            الحاجة للشراء أو التخزين. من الأدوات المنزلية إلى الخدمات المتخصصة،
            كل ما تحتاجه في متناول يدك.
          </p>
          <Stack direction={"horizontal"} className="gap-4">
            <LinkContainer
              to={{
                pathname: "/Results",
                search: "?type=product&PageNumber=1",
              }}
            >
              <Button className="text-white" variant="primary">
                تفقد المنتجات
              </Button>
            </LinkContainer>
            <LinkContainer
              to={{
                pathname: "/Results",
                search: "?type=service&PageNumber=1",
              }}
            >
              <Button className="text-white" variant="primary">
                تفقد الخدمات
              </Button>
            </LinkContainer>
          </Stack>
        </Col>
        <Col
          lg={4}
          md={5}
          sm={{ order: "last", span: "6" }}
          xs={{ order: "first", span: "12" }}
        >
          <Image fluid src={RentAnything} rounded />
        </Col>
      </Row>
    </Container>
  );
}
