import React, { Component } from "react";
import styled from "styled-components";

const Back = styled.div`
    display: flex; 
    margin-bottom: 400px;
    flex-direction: row;
    justify-content: center; 
    font-family: Apercu,"Source Sans Pro",system,system-ui,-apple-system,BlinkMacSystemFont,Roboto,Helvetica,Arial,sans-serif;
`;
const Paragraph = styled.p`
    margin-bottom: 25px; 
`;

const Content = styled.div`
    margin-left: 100px; 
    display: flex; 
    max-width: 30%;
    flex-direction: column;
    justify-content: center; 
`;

export class Background extends Component {
    render() {
        return (
            <Back>
                <h4 style={{ color: '#007bff' }}> Background </h4>
                <Content>
                    <Paragraph>I'm currently an Engineer at Upstatement building things for the web with some awesome people. I recently graduated from Northeastern University after completing three awesome six-month co-ops at MullenLowe U.S., Starry, and Apple Music.</Paragraph>
                    <Paragraph>As a software engineer, I enjoy bridging the gap between engineering and design — combining my technical knowledge with my keen eye for design to create a beautiful product. My goal is to always build applications that are scalable and efficient under the hood while providing engaging, pixel-perfect user experiences.</Paragraph>
                    <Paragraph>When I'm not in front of a computer screen, I'm probably snowboarding, cruising around on my penny board, or crossing off another item on my bucket list.</Paragraph>
                </Content>
            </Back>
        );
    }
}

export default Background;
