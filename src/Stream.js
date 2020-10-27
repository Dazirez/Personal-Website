import React, { Component } from "react";
import { TwitchEmbed } from "react-twitch-embed";
import styled from "styled-components";

const Container = styled.div`
    display: flex;
    justify-content: center;
    margin-top: 200px;
`;
export class Stream extends Component {
    render() {
        return (
            <React.Fragment>
                <Container>
                    <TwitchEmbed channel="loldreamer" withChat={true} />
                </Container>
            </React.Fragment>
        );
    }
}

export default Stream;
