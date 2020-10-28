import React, { Component } from 'react';
import { TwitchEmbed } from 'react-twitch-embed';
import styled from 'styled-components';

const Container = styled.div`
  padding-left: 75px;
  padding-right: 75px;
  margin-top: 25px;
`;
export class Stream extends Component {
  render() {
    return (
      <React.Fragment>
        <div id='Stream'>
          <h1 id='StreamHeader'>Hang Out With Me</h1>
          <Container>
            <TwitchEmbed
              width='auto'
              height='80vh'
              channel='loldreamer'
              withChat={true}
            />
          </Container>
        </div>
      </React.Fragment>
    );
  }
}

export default Stream;
