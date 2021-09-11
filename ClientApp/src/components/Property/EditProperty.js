import { post } from 'jquery';
import React, { useState } from 'react';
import ImageUploadArea from '../ImageUploadArea';
import Popup from '../Popup';

export class EditProperty extends React.Component {
    constructor(props) {
        super(props);
        const session = JSON.parse(localStorage.getItem("user_session")).userObj;
        this.id = props.match.params.id;
        this.state = {
            currentUser: { name: session.name, token: session.token },
            post: {
                Id: 0,
                PropertyType: "",
                Description: "",
                Address: "",
                CreatedUser: session.name,
                UpdatedUser: session.name,
                ImageUrl: ""
            },
            popup: {
                type: "success",
                message: "",
                visible: false
            },
            images: []
        }

        this.sign = this.sign.bind(this);
        this.popDismiss = this.popDismiss.bind(this);
        this.onUpload = this.onUpload.bind(this);
        this.removeImage = this.removeImage.bind(this);
        if (this.id > 0) {
            this.getProperyById(this.id);
        }
    }

    componentDidMount() {
        if (document.getElementById("PropertyType")) {
            document.querySelectorAll("input").forEach(item => {
                item.setAttribute("autocomplete", "off");
            });

            document.getElementById("images").addEventListener("change", (e) => {
                var reader = new FileReader();
                reader.onload = (base) => {
                    this.setState({ images: [...this.state.images, { Url: base.target.result }] });
                    document.getElementById("images").value = "";
                }
                reader.readAsDataURL(e.target.files[0]);
            });
        }
    }

    getProperyById(id) {
        const Options = {
            method: "GET",
            headers: { 'Content-Type': 'application/json' }
        }

        fetch("api/property/" + id, Options)
            .then(response => response.json())
            .then(data => {
                if (data) {
                    this.setState({
                        post: {
                            Id: id,
                            PropertyType: data.propertyType,
                            Description: data.description,
                            Address: data.address,
                            CreatedUser: data.createdUser,
                            UpdatedUser: data.updatedUser,
                            ImageUrl: ""
                        }
                    });

                    if (data.files.length > 0) {
                        data.files.forEach(x => {
                            this.setState({ images: [...this.state.images, { Url: x.url }] })
                        });
                    }
                }
            }).catch(err => { alert(err) });
    }

    sign(e) {
        e.preventDefault();
        var id = this.state.post.Id;
        var url = "api/property";
        if (id > 0) {
            url = "api/property/" + id;
            this.setState({ post: { ...this.state.post, UpdatedUser: this.state.currentUser.name } });
        }

        const requestOptions = {
            method: id > 0 ? 'PUT' : 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ...this.state.post, Files: this.state.images })
        }

        fetch(url, requestOptions)
            .then(response => {
                if (response.status == 201) {
                    this.setState({
                        post: {
                            Id: 0,
                            PropertyType: "",
                            Description: "",
                            Address: "",
                            CreatedUser: this.state.currentUser.name,
                            UpdatedUser: this.state.currentUser.name,
                            ImageUrl: ""
                        }
                    });
                    this.setState({ popup: { type: "success", message: "Imovel criado com sucesso!", visible: true } });
                } else if (id > 0 && response.status == 204) {
                    this.setState({ popup: { type: "success", message: "Imovel actualizado com sucesso!", visible: true } });
                }
            }).catch(err => { alert(err) });
    }

    popDismiss() {
        this.setState({ popup: { ...this.state.popup, visible: false } });
    }

    onUpload() {
        document.getElementById('images').click();
    }

    removeImage(index) {
        this.setState({ images: this.state.images.filter(x => this.state.images.indexOf(x) != index) });
    }

    render() {
        return (
            <>
                <Popup type={this.state.popup.type} visible={this.state.popup.visible} message={this.state.popup.message} remove={this.popDismiss} />
                <ImageUploadArea source={this.state.images} bindclick={this.onUpload} onDelete={this.removeImage} />
                <div className="addProperty">
                    <div className="addPropertyForm">
                        <form onSubmit={this.sign}>
                            <label>Property Type</label>
                            <input required id="PropertyType" type="text" className="form-control" value={this.state.post.PropertyType} onChange={(e) => this.setState({ post: { ...this.state.post, PropertyType: e.target.value } })} />

                            <label>Description</label>
                            <input id="Description" type="text" className="form-control" value={this.state.post.Description} onChange={(e) => this.setState({ post: { ...this.state.post, Description: e.target.value } })} />

                            <label>Address</label>
                            <input id="Address" type="text" className="form-control" value={this.state.post.Address} onChange={(e) => this.setState({ post: { ...this.state.post, Address: e.target.value } })} />

                            <br></br>
                            <input id="images" type="file" accept="image/*" style={{ display: "none" }} />
                            <button type="submit" className="btn btn-block">Guardar</button>
                        </form>
                    </div>
                </div>
            </>
        );
    }
}