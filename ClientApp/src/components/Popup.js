import React, { useState } from "react";
import { AiFillCloseCircle } from "react-icons/ai";

const Popup = ({ type, message, visible, remove }) => {
    const style = {
        position: "absolute",
        right: "10px",
        top: "9px",
        cursor: "pointer"
    }
    return (
        <div className="pop" style={{ display: visible ? "block" : "none" }}>
            <div className={"alert alert-" + type}>
                {message}
                <div style={style} onClick={() => { remove(); }}>
                 <AiFillCloseCircle size="25px"/> 
                </div>
            </div>
        </div>
    )
}

export default Popup;