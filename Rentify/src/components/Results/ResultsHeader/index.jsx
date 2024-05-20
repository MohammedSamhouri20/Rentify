import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import SortingOptions from "@/components/SortingOptions";
export default function ResultsHeader() {
  return (
    <Row className="p-4 gap-3 shadow justify-content-md-between align-items-center">
      <Col xs={12} lg={4} className="">
        <h5
          style={{
            width: "fit-content",
          }}
          className="m-auto m-lg-0"
        >
          عدد النتائج الظاهرة <span className="text-primary">03</span> من
          <span className="text-primary"> 175</span>
        </h5>
      </Col>
      <Col xs={12} lg={7} className="">
        <Row className="justify-content-center gap-2  align-items-center">
          <Col xs={12} xl={2} lg={2} className="text-center">
            ترتيب حسب
          </Col>
          <Col xs={12} xl={9} lg={9}>
            <Row className="gap-1 align-items-center justify-content-evenly">
              <SortingOptions />
              <SortingOptions />
            </Row>
          </Col>
        </Row>
      </Col>
    </Row>
  );
}
