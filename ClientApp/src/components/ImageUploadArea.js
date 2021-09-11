import React, { useState } from "react";
import { MdCloudUpload } from "react-icons/md";
import { AiFillCloseCircle, AiOutlineDelete } from "react-icons/ai";
const ImageUploadArea = ({ source, bindclick, onDelete }) => {
    return (
        <div className="area">
            {source.map(i =>
                <div className="imageArea" key={source.indexOf(i) + 1} style={{ backgroundImage: `url(${i.Url})` }}>
                    <AiFillCloseCircle size="25px" onClick={() => onDelete(source.indexOf(i))} />
                </div>
            )}
            <div className="addImage" onClick={() => bindclick()}>
                <MdCloudUpload size="50px" />
                <p>Upload</p>
            </div>
            <div style={{ clear: "both" }}></div>
        </div>
    )
}

export default ImageUploadArea;