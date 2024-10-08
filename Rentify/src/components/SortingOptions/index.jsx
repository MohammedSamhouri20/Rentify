import { useState } from "react";
import Dropdown from "react-bootstrap/Dropdown";
export default function SortingOptions() {
  const [selectedOption, setSelectedOption] = useState(0);

  const options = ["من الأحدث للأقدم", "من الأقدم للأحدث"];

  const changeActiveOption = (e) => {
    setSelectedOption(e);
  };
  return (
    <div
      style={{ maxWidth: "240px" }}
      className="d-flex gap-2 align-items-center"
    >
      <div className="text-nowrap ">ترتيب</div>
      <Dropdown className="w-100" onSelect={changeActiveOption}>
        <Dropdown.Toggle className="text-white w-100 pe-start">
          {options[selectedOption]}
        </Dropdown.Toggle>
        <Dropdown.Menu>
          {options.map((option, index) => (
            <Dropdown.Item
              eventKey={index}
              key={index}
              active={index == selectedOption}
            >
              {option}
            </Dropdown.Item>
          ))}
        </Dropdown.Menu>
      </Dropdown>
    </div>
  );
}
