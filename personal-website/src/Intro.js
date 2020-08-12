import React, { Component } from "react";
import styled from "styled-components";



const Introduction = styled.header`
    width: 100%;
    padding: 120px 100px; 
    display: flex; 
    justify-content: space-around; 
    height: 100vh;
    font-family: Apercu,"Source Sans Pro",system,system-ui,-apple-system,BlinkMacSystemFont,Roboto,Helvetica,Arial,sans-serif;
`;
const Hello = styled.h1`
    display: block;
    padding: 80px 200px; 
`;
const Description = styled.h2`
    display: block;
    padding: 80px 200px; 
`;
const Contact = styled.h3`
    display: block;
    padding: 80px 200px; 
`;
const Text = styled.body`
    font-family: Apercu,"Source Sans Pro",system,system-ui,-apple-system,BlinkMacSystemFont,Roboto,Helvetica,Arial,sans-serif;
    line-height: 1.5;
`;
const Propic = styled.img`
    float: right; 
    width: 300px; 
    height: 300px; 
    padding: 0px 80px; 
`;

export class Intro extends Component {
    render() {
        return (
            <Introduction>
                <Text>
                    <Hello>Hello</Hello>
                    <Description>I'm Daniel, a Prospective Software Developer from Michigan. I’m currently studying computer science at the University of Michigan- Ann Arbor.

                    I love making music and experiencing new things. I play the guitar and piano, and am seriously curious about anything that comes my way.

                    If you’d like to make something happen, feel free to get in touch with me 🙂<Propic src={require("./assets/img/Propic.png")} /></Description>


                    <Contact>Get in Touch</Contact>
                </Text>
            </Introduction>
        );
    }
}

export default Intro;
