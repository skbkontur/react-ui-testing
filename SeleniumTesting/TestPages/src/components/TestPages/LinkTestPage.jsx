import React from 'react';
import Button from 'retail-ui/components/Button'
import Link from 'retail-ui/components/Link'
import { CaseSuite, Case } from '../Case';

export default class LinkTestPage extends React.Component {
    state = {
        value: null,
    };

    render(): React.Element<*> {
        return (
            <CaseSuite title='Link'>
                <Case title='Simple Link'>
                    <Case.Body>
                        <Link
                            href='#'
                            data-tid='SimpleLink'
                        >
                            Simple link
                        </Link>
                    </Case.Body>
                </Case>
           </CaseSuite>
        );
    }
}