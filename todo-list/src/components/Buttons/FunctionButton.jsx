import React from "react";

const FunctionButton = (props) => {
  return (
    <button className={`btn btn-${props.tipo}`} onClick={props.callback}>
      {props.text}
    </button>
  );
};

export default FunctionButton;