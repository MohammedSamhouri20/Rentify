import { Container, Row, Col, Stack, Image } from "react-bootstrap";
import WhyUsBackground from "@/assets/images/why-us/why-us-bg.png";
import Guarantee from "@/assets/images/why-us/guarantee.png";
import Money from "@/assets/images/why-us/money.png";
import Environment from "@/assets/images/why-us/environment.png";
import SectionLine from "@/components/SectionLine";
export default function WhyChooseUsSection() {
  const WhyUs = [
    {
      id: 1,
      title: "أجّر بأمان",
      icon: Guarantee,
    },
    {
      id: 2,
      title: "وفّر أموالك",
      icon: Money,
    },
    {
      id: 3,
      title: "حافظ على البيئة",
      icon: Environment,
    },
  ];

  return (
    <>
      <Container
        className="p-5 d-flex flex-column bg-primary align-items-center"
        fluid
        style={{
          backgroundImage: `url(${WhyUsBackground})`,
          borderRadius: "30px",
        }}
      >
        <Row className="flex-column align-items-center ">
          <h1 className="text-white text-center"> لماذا Rentify؟</h1>

          <SectionLine backgroundColor="bg-white" />

          <p className="text-light text-center text-opacity-75">
            ما الذي يميّز Rentify عن غيره؟
          </p>
        </Row>

        <Row className="flex-column flex-lg-row gap-4 align-items-center  justify-content-center w-100 m-2">
          {WhyUs.map((step) => (
            <Col
              xs={12}
              lg={2}
              className="d-flex flex-column text-nowrap  justify-content-center align-items-center"
              key={step.id}
            >
              <Image
                className="bg-white p-2"
                roundedCircle
                draggable={false}
                style={{ width: "80px" }}
                src={step.icon}
                alt="Step"
              />

              <h4 className="fw-normal text-white mt-3">{step.title}</h4>
            </Col>
          ))}
        </Row>
      </Container>
    </>
  );
}
